using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Models;
using System.Configuration;

namespace WebApiEncuesta.Controllers.Repositories
{
    public class PreguntasRepository : IPreguntas
    {
        #region Variables globales
        Context _context = null;
        #endregion

        public PreguntasRepository()
        {
            this._context = new Context();   
        } 

      
        public Preguntas addPregunta(Preguntas p)
        {
            string exception = String.Empty;

         
            _context.Preguntas.Add(p);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                p = new Preguntas() { IdPregunta = -2 };
            }
          
            return p;
        }


        private void ordenarPreguntas(int idEncuesta){
            var listPre = findPreguntasXEncuesta(idEncuesta);
            int i = 1;
            foreach (Preguntas p in listPre)
            {
                p.Orden = i;
                i++;
               _context.SaveChanges();
            }
        }


        public Preguntas updatePregunta(Preguntas p)
        {
            string exception = String.Empty;

            var pregunta = (from pre in _context.Preguntas
                            where pre.IdPregunta == p.IdPregunta
                            select pre);

            foreach (var set in pregunta)
            {
                set.Estado = p.Estado;
                set.IdPreguntaPadre = p.IdPreguntaPadre;
                set.Nombre = p.Nombre;
                set.Orden = p.Orden;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                p = new Preguntas() { IdPregunta = -2 };
            }
            

            return p;
        }


        public Preguntas findPreguntaXid(int id)
        {
            return _context.Preguntas.Find(id);
        }

        public IEnumerable<Preguntas> findAllPreguntas()
        {
            var listPreguntas = (from p in _context.Preguntas
                                 select p
                                ).ToList();
            return listPreguntas;
        }


        public dynamic findPreguntasXEncuesta(int idEncuesta)
        {
            var listPreguntas = (from p in _context.Preguntas
                                    where p.IdEncuesta == idEncuesta
                                 select p).ToList();
            return listPreguntas;
        }


        public Preguntas findPreguntaXNombre(string nombre, int idEncuesta)
        {
            var pregunta = (from p in _context.Preguntas
                            where p.Nombre.ToUpper() == nombre.ToUpper() &&
                                  p.IdEncuesta == idEncuesta
                            select p
                                 ).FirstOrDefault();
            return pregunta;
        }


        public dynamic findPreguntasToDataTable(int idEncuesta)
        {
            var listDynamic = (
                    from p in _context.Preguntas
                    join cw in _context.ControlWeb on new { IdPregunta = p.IdPregunta } equals new { IdPregunta = (cw.IdPregunta ?? (System.Int32)0) } into cw_join
                    from cw in cw_join.DefaultIfEmpty()
                    where
                      p.Encuesta.IdEncuesta == idEncuesta
                    select new
                    {
                        p.Encuesta.Nombre,
                        p.Encuesta.Descripcion,
                        p.IdPregunta,
                        NombrePregunta = p.Nombre,
                        p.Orden,
                        Estado = p.Estado == true ? "Activo" : "Inactivo",
                        ElementoHtml5 = cw.ElementoHtml5 == null ? "N/A" : cw.ElementoHtml5,
                        TipoElementoHtml5 = cw.TipoElementoHtml5 == null ? "N/A" : cw.TipoElementoHtml5,
                        ValorXDefecto = cw.ValorXDefecto == null ? "N/A" : cw.ValorXDefecto,
                        RequeridoS = cw.Requerido == true ? "Requerido" : "No Requerido"
                    }
                ).ToList();
            return listDynamic;
        }


        public bool deletePrgunta(int idPregunta)
        {
            #region EliminarSubPreguntas

                var listSubPreguntas = (from subP in _context.Preguntas
                                        where (subP.IdPreguntaPadre ?? (System.Int32)0) == idPregunta
                                        select subP).ToList();

                if (listSubPreguntas != null)
                {
                    if (listSubPreguntas.Count() > 0)
                    {
                        foreach (Preguntas pre in listSubPreguntas)
                        {
                            deleteControlWebAndOptionsRta(pre);
                        }
                    }
                }

            #endregion

            #region EliminarPreguntaPadre

                var preguntaPadre = (from p in _context.Preguntas
                                     where p.IdPregunta == idPregunta
                                     select p).Single();
                if (preguntaPadre != null)
                {
                    deleteControlWebAndOptionsRta(preguntaPadre);  
                }

            #endregion

            return true;
        }


        private void deleteControlWebAndOptionsRta(Preguntas pre)
        {
            var controlWebs = (from cw in _context.ControlWeb
                               where cw.IdPregunta == pre.IdPregunta
                               select cw).FirstOrDefault();
            if (controlWebs != null)
            {
              if(controlWebs.IdControlWeb > 0){
                  var optionRta = (from oRta in _context.OpcionesRespuesta
                                   where oRta.IdControlWeb == controlWebs.IdControlWeb
                                   select oRta).ToList();
                  if (optionRta != null)
                  {
                      if (optionRta.Count() > 0)
                      {
                          foreach (OpcionesRespuesta rta in optionRta)
                          {
                              _context.OpcionesRespuesta.Attach(rta);
                              _context.OpcionesRespuesta.Remove(rta);
                              _context.SaveChanges();
                          }
                      }
                  }
                  _context.ControlWeb.Attach(controlWebs);
                  _context.ControlWeb.Remove(controlWebs);
                  _context.SaveChanges();
                }
            }
            _context.Preguntas.Attach(pre);
            _context.Preguntas.Remove(pre);
            _context.SaveChanges();
        }
    }
}