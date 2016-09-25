using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;
using System.Web.Hosting;


namespace WebApiEncuesta.Controllers.Repositories
{
    public class EncuestaRepository : IEncuesta
    {
       
        Context _context;

        public EncuestaRepository()
        {
            this._context = new Context();
        }

        public Encuesta addEncuesta(Encuesta encuesta)
        {
            string exception = String.Empty;

           

            encuesta.Fecha = DateTime.Now;
            _context.Encuesta.Add(encuesta);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                encuesta = new Encuesta() { IdEncuesta = -2 };
            }

            return encuesta;
        }

        public Encuesta updateEncuesta(Encuesta encuesta)
        {
            string exception = String.Empty;

            var sql = from e in _context.Encuesta
                      where e.IdEncuesta == encuesta.IdEncuesta
                      select e;

            
            foreach (var set in sql)
            {
                set.Estado = encuesta.Estado;
                set.Nombre = encuesta.Nombre;
                set.Descripcion = encuesta.Descripcion;
                set.IdEncuestaRedirec = encuesta.IdEncuestaRedirec;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                encuesta = new Encuesta() { IdEncuesta = -5 };
            }
           
            return encuesta;
        }


        public Encuesta findEncuestaXNombre(string nombre, int idSegmento)
        {
            var encuesta = (from e in _context.Encuesta
                            where e.Nombre.Contains(nombre) &&
                                  e.IdSegmento == idSegmento
                            select e).FirstOrDefault();
            return encuesta;
        }


        public Encuesta findEncuestaXId(int id)
        {
            return _context.Encuesta.Find(id);
        }


        public IEnumerable<Encuesta> findAllEncuestas()
        {
            var ListEncuestas = (from e in _context.Encuesta select e).ToList();
            return ListEncuestas;
        }


        public IEnumerable<Encuesta> findAllEncuestasXEstado(bool estado)
        {
            var listEncuesta = (from e in _context.Encuesta
                                where e.Estado == estado
                                select e).ToList();
            return listEncuesta;
        }

 
        bool IEncuesta.findEncuestaXNombreBool(string nombre, int idSegmento)
        {
            var encuesta = (from e in _context.Encuesta
                            where e.Nombre.Contains(nombre) &&
                                   e.IdSegmento == idSegmento
                            select e).ToList();
            return (encuesta.Count() > 0);
        }


        public dynamic findAllEncuestasListDynamic(int idsegmento)
        {
            var list = (from e in _context.Encuesta
                        where e.IdSegmento == idsegmento
                        select new
                        {
                            e.IdEncuesta,
                            EstadoString = e.Estado == true ? "Activo" : "Inactivo",
                            e.Nombre,
                            e.Descripcion,
                            e.Fecha
                        }).ToList();
            return list;
        }

        public dynamic findEncuentaPreguntaXSegId(int idSegmento, int idEncuesta)
        {

            List<FindEncuestas> listFEncuestas = new List<FindEncuestas>();

            var listEncuestas = (from p in _context.Preguntas
                                 join e in _context.Encuesta on p.IdEncuesta equals e.IdEncuesta
                                 join cWeb in _context.ControlWeb on p.IdPregunta equals cWeb.IdPregunta into controlsWebs
                                 from cWeb in controlsWebs.DefaultIfEmpty()
                                 where e.IdSegmento == idSegmento &&
                                      e.IdEncuesta == idEncuesta &&
                                      p.Estado == true &&
                                      p.IdPreguntaPadre == null
                                 select new
                                 {
                                     p.IdPregunta,
                                     p.Nombre,
                                     p.Orden,
                                     IdPreguntaPadre = p.IdPreguntaPadre == null ? 0 : p.IdPreguntaPadre,
                                     ObjControlWeb = cWeb,
                                     ListObjOpcRta = _context.OpcionesRespuesta.Where(y => y.IdControlWeb == (cWeb == null ? 0 : cWeb.IdControlWeb)).Distinct().OrderBy(h => h.Orden).ToList()
                                 }
                                  ).OrderBy(g => g.Orden).ToList();

            foreach (var prePadre in listEncuestas)
            {
                listFEncuestas.Add(new FindEncuestas
                {
                    IdPregunta = prePadre.IdPregunta,
                    Nombre = prePadre.Nombre,
                    Orden = prePadre.Orden,
                    IdPreguntaPadre = prePadre.IdPreguntaPadre,
                    ListPreHijas = recursivoBusquedaHijas(prePadre.IdPregunta),
                    ObjControlWeb = prePadre.ObjControlWeb,
                    listOpc = prePadre.ListObjOpcRta
                });
            }



            return listFEncuestas;
        }


        private List<FindEncuestas> recursivoBusquedaHijas(int idPadre)
        {

            List<FindEncuestas> listFEncuestas = new List<FindEncuestas>();

            var listEncuestas = (from p in _context.Preguntas
                                 join e in _context.Encuesta on p.IdEncuesta equals e.IdEncuesta
                                 join cWeb in _context.ControlWeb on p.IdPregunta equals cWeb.IdPregunta into controlsWebs
                                 from cWeb in controlsWebs.DefaultIfEmpty()
                                 where
                                      p.Estado == true &&
                                      p.IdPreguntaPadre == idPadre
                                 select new
                                 {
                                     p.IdPregunta,
                                     p.Nombre,
                                     p.Orden,
                                     IdPreguntaPadre = p.IdPreguntaPadre == null ? 0 : p.IdPreguntaPadre,
                                     ObjControlWeb = cWeb,
                                     ListObjOpcRta = _context.OpcionesRespuesta.Where(y => y.IdControlWeb == (cWeb == null ? 0 : cWeb.IdControlWeb)).Distinct().OrderBy(h => h.Orden).ToList()
                                 }
                                  ).OrderBy(g => g.Orden).ToList();


            if (listEncuestas != null && listEncuestas.Count() > 0)
            {
                foreach (var encuesta in listEncuestas)
                {

                    listFEncuestas.Add(new FindEncuestas
                    {
                        IdPregunta = encuesta.IdPregunta,
                        Nombre = encuesta.Nombre,
                        Orden = encuesta.Orden,
                        IdPreguntaPadre = encuesta.IdPreguntaPadre,
                        ListPreHijas = recursivoBusquedaHijas(encuesta.IdPregunta),
                        ObjControlWeb = encuesta.ObjControlWeb,
                        listOpc = encuesta.ListObjOpcRta
                    });
                }
            }
            else
            {
                return null;
            }
            return listFEncuestas;
        }


        public dynamic findEncustasXSegmentoEstado(int idSegmento)
        {
            var listEncuestas = (from en in _context.Encuesta
                                 where en.Estado == true &&
                                       en.IdSegmento == idSegmento
                                 select new
                                 {
                                     en.Descripcion,
                                     en.IdEncuesta
                                 }).Distinct().ToList();
            return listEncuestas;
        }


        public dynamic findPollXPregunta(int idPregunta)
        {
            var objEncuesta = (
                                from p in _context.Preguntas
                                where p.IdPregunta == idPregunta
                                select new
                                {
                                    IdEncuestaRedirec = p.Encuesta.IdEncuestaRedirec == null ? -1 : p.Encuesta.IdEncuestaRedirec
                                }
                               ).Distinct().ToList();
            return objEncuesta;
        }

        public dynamic ConsultaRespuestaNo(int idProyecto, int Id, string OpRespuesta, int idEncuesta)
        {
            var consulta1 = (from EncuestaXReporteEnlaces in _context.EncuestaXReporteEnlace
                             where
                               EncuestaXReporteEnlaces.IdCampo == idProyecto
                             orderby
                               EncuestaXReporteEnlaces.IdEncuestaXReporteEnlace descending
                             select new
                             {
                                 FechaFinal = EncuestaXReporteEnlaces.FechaFin,
                                 EncuestaXReporteEnlaces.FechaInicial
                             }).FirstOrDefault();

            var ConsultaNo = (from p in _context.RespuestaXPreguta
                              where
                                  (from PreguntasN1 in _context.Preguntas
                                   where
                                     PreguntasN1.IdEncuesta == idEncuesta && p.fechaRegistro >= consulta1.FechaInicial && p.fechaRegistro <= consulta1.FechaFinal && p.OpcionesRespuesta.Nombre == OpRespuesta && p.IdUsuario == Id.ToString()
                                   select new
                                   {
                                       PreguntasN1.IdPregunta
                                   }).Contains(new { IdPregunta = p.IdPregunta })
                              select new
                              {
                                  p.OpcionesRespuesta.Nombre
                              }).ToList();
            int result = ConsultaNo.Count;

            return result;
        }
    }

    public class FindEncuestas
    {
        public int IdPregunta { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int? IdPreguntaPadre { get; set; }
        public List<FindEncuestas> ListPreHijas { get; set; }
        public ControlWeb ObjControlWeb { get; set; }
        public List<OpcionesRespuesta> listOpc { get; set; }
    }

}
