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

    public class ControlWebController : ApiController
    {
       IControlWeb _icontrol = (IControlWeb) new ControlWebRepository();
        public IHttpActionResult Get(string filtro = "", string nombre = "", bool estado = false, int id = 0)
        {

            switch (filtro)
            {
                case "ControlWebXElementoAndTipo":
                    return Ok(_icontrol.findElementsHtml5TipoElement());
                default:
                    return Ok(_icontrol.findAllControlWeb());
            }
        }

        public IHttpActionResult Post(ControlWeb cw)
        {
            try
            {
                return Ok(_icontrol.addControlWeb(cw));     
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        public IHttpActionResult Put(ControlWeb cw)
        {
            ControlWeb controlW = null;
            try
            {

                controlW = _icontrol.findControlWebXid(cw.IdControlWeb);

                if (controlW != null)
                {
                    _icontrol.updateControlWeb(cw);
                    return Ok(200);
                }
                else
                {
                    //Si se presenta el codigo 302 es porque el control web  no existe
                    return Ok(302);
                }
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        public IHttpActionResult Delete(ControlWeb cw)
        {
            try
            {
                _icontrol.deleteControlWeb(cw.IdControlWeb);
                return Ok(200);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

    }
}
