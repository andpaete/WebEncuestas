using System.Web.Http;
using System.Web.Http.Cors;
using System;
using System.Net;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Controllers.Repositories;

namespace UniversalAPI.Controllers
{

    [EnableCors(origins: "http://localhost:22372,http://localhost:22373", headers: "*", methods: "*")]

    public class CssControlN1Controller : ApiController
    {

        ICssControl _icss = (ICssControl)new CssControlRepository();

        public IHttpActionResult GET(string filtro = "", int id = 0, string tipoControl = "")
        {
            switch (filtro)
            {
                case "css":
                    return Ok(_icss.findAllCss());
                case "javascript":
                    return Ok(_icss.findAllJavascript(id));
                case "tipoControl":
                    return Ok(_icss.findCssXTipoControl(tipoControl));
                default:
                    return null;
            }
        }

    }
}
