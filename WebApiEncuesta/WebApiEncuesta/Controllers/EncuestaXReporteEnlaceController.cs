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

namespace WebApiEncuesta.Controllers
{

    [EnableCors(origins: "http://localhost:22372,http://localhost:22373", headers: "*", methods: "*")]

    public class EncuestaXReporteEnlaceController : ApiController
    {

        private IEncuestaXReporteEnlace _IEncuestaReporte = (IEncuestaXReporteEnlace)new EncuestaXReporteEnlaceRepository();

        public IHttpActionResult Post(EncuestaXReporteEnlace encuesXReport)
        {
            try
            {
                EncuestaXReporteEnlace r = _IEncuestaReporte.addEncuestaXReporteEnlace(encuesXReport);
                if (r.IdEncuestaXReporteEnlace == -2)
                {
                    return Ok(400);
                }
                return Ok(r);
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

    }
}
