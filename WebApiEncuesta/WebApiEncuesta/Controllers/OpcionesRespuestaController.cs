using System.Web.Http;
using System.Web.Http.Cors;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Controllers.Repositories;
using System;
using System.Net;

namespace WebApiEncuesta.Controllers
{

    [EnableCors(origins: "http://localhost:22372,http://localhost:22373", headers: "*", methods: "*")]

    public class OpcionesRespuestaController : ApiController
    {

      IOpcionesRespuesta _op = (IOpcionesRespuesta)new OpcionesRespuestaRepository();

      public IHttpActionResult Post(OpcionesRespuesta op)
      {
          try
          {
              return Ok(_op.addOpcionesRespuesta(op));
          }
          catch (Exception)
          {
              //Si se presenta el codigo 400 significa ocurrio un error.
              return StatusCode(HttpStatusCode.BadRequest);
          }
      }

    }
}
