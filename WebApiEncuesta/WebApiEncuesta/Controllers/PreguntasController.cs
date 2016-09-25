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

    public class PreguntasController : ApiController
    {

       IPreguntas _ipreguntas = (IPreguntas) new PreguntasRepository();

        public IHttpActionResult Get(string filtro = "", string nombre = "", bool estado = false, int id = 0,int idsegmento = 0,int idEncuesta = 0)
        {

            switch (filtro)
            {
                case "PreguntaXnombre":
                    return Ok(_ipreguntas.findPreguntaXNombre(nombre,idEncuesta));
                case "PreguntasDataTable":
                    return Ok(_ipreguntas.findPreguntasToDataTable(idEncuesta));
                case "PreguntasXid":
                    return Ok(_ipreguntas.findPreguntaXid(id));
                default:
                    return Ok(_ipreguntas.findAllPreguntas());
            }
        }

        public IHttpActionResult Post(Preguntas p)
        {
            Preguntas pre = null;
            try
            {
                pre = _ipreguntas.findPreguntaXNombre(p.Nombre, p.IdEncuesta);

                if (pre == null)
                {
                    return Ok(_ipreguntas.addPregunta(p));
                }
                else
                {
                    //Si se presenta el codigo 300 significa que la pregunta ya existe.
                    return Ok(203);
                }
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        public IHttpActionResult Put(Preguntas p)
        {
            Preguntas pre = null;
            try
            {
                pre = _ipreguntas.findPreguntaXid(p.IdPregunta);

                if (pre != null)
                {
                    _ipreguntas.updatePregunta(p);
                    return Ok(200);
                }
                else
                {
                    //Si se presenta el codigo 302 es porque la pregunta no existe
                    return Ok(302);
                }
            }
            catch (Exception)
            {
                //Si se presenta el codigo 400 significa ocurrio un error.
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        public IHttpActionResult Delete(Preguntas p)
        {
            try
            {    //las preguntas no se deben eliminar solo cambian de estado
                //return Ok(_ipreguntas.deletePrgunta(p.IdPregunta));
                Preguntas pre = _ipreguntas.findPreguntaXid(p.IdPregunta);
                pre.Estado = false;
                _ipreguntas.updatePregunta(pre);
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

    }
}
