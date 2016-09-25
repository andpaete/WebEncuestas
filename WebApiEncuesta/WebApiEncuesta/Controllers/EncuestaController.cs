using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Controllers.Repositories;

namespace UniversalAPI.Controllers
{

    [EnableCors(origins: "http://localhost:22372,http://localhost:22373", headers: "*", methods: "*")]

    public class EncuestaController : ApiController
    {
        private IEncuesta _encuesta = (IEncuesta)new EncuestaRepository();

        public IHttpActionResult Get(string filtro = "", string nombre = "", bool estado = false, int id = 0, int idsegmento = 0, int idEncuesta = 0, int idPregunta = 0, int idProyecto = 0)
        {

            switch (filtro)
            {
                case "EncuestasXNombre":
                    return Ok(_encuesta.findEncuestaXNombre(nombre, idsegmento));
                case "AllEncuestasXEstado":
                    return Ok(_encuesta.findAllEncuestasXEstado(estado));
                case "AllEncuestasListDynamic":
                    return Ok(_encuesta.findAllEncuestasListDynamic(idsegmento));
                case "findXNombre":
                    return Ok(_encuesta.findEncuestaXNombre(nombre, idsegmento));
                case "findXID":
                    return Ok(_encuesta.findEncuestaXId(id));
                case "findEncuentaPreguntaXSegId":
                    return Ok(_encuesta.findEncuentaPreguntaXSegId(idsegmento, idEncuesta));
                case "findEncuestaXSegEstado":
                    return Ok(_encuesta.findEncustasXSegmentoEstado(idsegmento));
                case "findPollXPregunta":
                    return Ok(_encuesta.findPollXPregunta(idPregunta));
                case "ConsultaRespuestaNo":
                    return Ok(_encuesta.ConsultaRespuestaNo(idProyecto, id, nombre, idEncuesta));
                default:
                    return Ok(_encuesta.findAllEncuestas());
            }
        }

        public IHttpActionResult Post(Encuesta encuesta)
        {
            try
            {
                bool estadoExist = _encuesta.findEncuestaXNombreBool(encuesta.Nombre.Trim().ToUpper(), encuesta.IdSegmento);

                if (!estadoExist)
                {
                    _encuesta.addEncuesta(encuesta);
                    return Ok(200);
                }
                else
                {
                    //Si se presenta el codigo 203 significa que la encuesta   ya existe.
                    return Ok(203);
                }
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        public IHttpActionResult Put(Encuesta encuesta)
        {
            Encuesta e = null;
            try
            {

                e = _encuesta.findEncuestaXId(encuesta.IdEncuesta);

                if (e != null)
                {
                    _encuesta.updateEncuesta(encuesta);
                    return Ok(200);
                }
                else
                {
                    //Si se presenta el codigo 302 es porque la boleta no existe
                    return Ok(302);
                }
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

    }
}
