using System.Web.Http;
using System.Web.Http.Cors;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Controllers.Repositories;
using System;
using System.Net;
using System.Collections.Generic;

namespace UniversalAPI.Controllers
{

    [EnableCors(origins: "http://localhost:22372,http://localhost:22373", headers: "*", methods: "*")]

    public class RespuestaXPregutaController : ApiController
    {

        IRespuestaXPreguta _irxp = (IRespuestaXPreguta)new RespuestaXPregutaRepository();

        public IHttpActionResult Get(string filtro = "", int idCampo = 0)
        {
            switch (filtro)
            {
                case "getEncuestasXProyecto":
                    return Ok(_irxp.getEncuestasXProyecto(idCampo));
                default:
                    return Ok("");
            }

        }

        public IHttpActionResult Post(List<RespuestaXPreguta> listRXP)
        {
            try
            {
                if (_irxp.addListRespuestaXPreguta(listRXP))
                {
                    return Ok(200);
                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
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
