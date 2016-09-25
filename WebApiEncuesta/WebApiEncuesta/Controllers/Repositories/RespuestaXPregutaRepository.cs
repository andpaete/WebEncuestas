using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Models;

using System.Configuration;

namespace WebApiEncuesta.Controllers.Repositories
{
 
    public class RespuestaXPregutaRepository : IRespuestaXPreguta
    {

        #region Variables globales
        Context _context = null;
        #endregion


        public RespuestaXPregutaRepository()
        {
            this._context = new Context();   
        }

       
        public RespuestaXPreguta addRespuestaXPreguta(RespuestaXPreguta rxp)
        {
            string exception = String.Empty;
            DateTime fechaRegistro = DateTime.Now;
            rxp.fechaRegistro = fechaRegistro;

            _context.RespuestaXPreguta.Add(rxp);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                rxp = new RespuestaXPreguta() { IdRespuestaXPregunta = -2 };
            }
            
            return rxp;
        }

        public bool addListRespuestaXPreguta(List<RespuestaXPreguta> listRespuestaXPreguta)
        {
            RespuestaXPreguta rxpIdSave;
            if (listRespuestaXPreguta != null && listRespuestaXPreguta.Count() > 0)
            {
                foreach (RespuestaXPreguta rxp in listRespuestaXPreguta)
                {
                    rxpIdSave = null;
                    rxpIdSave = addRespuestaXPreguta(rxp);
                    if (rxpIdSave.IdRespuestaXPregunta == -2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public dynamic getEncuestasXProyecto(int idCampo)
        {
            var listRespuestas = (from rxp in _context.RespuestaXPreguta
                                  join rEnlace in _context.EncuestaXReporteEnlace on rxp.IdReporteEnlaceEncuesta equals rEnlace.IdEncuestaXReporteEnlace
                                  where rEnlace.IdCampo == idCampo
                                  select new
                                  {
                                      NombreEncuesta = rxp.Preguntas.Encuesta.Nombre,
                                      NombrePregunta = rxp.Preguntas.Nombre,
                                      Respuesta = rxp.OpcionesRespuesta.Nombre
                                  }).Distinct().ToList();
            return listRespuestas;
        }
    }
}