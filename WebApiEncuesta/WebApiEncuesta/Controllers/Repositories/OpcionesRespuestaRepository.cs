using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;

namespace WebApiEncuesta.Controllers.Repositories
{
    public class OpcionesRespuestaRepository : IOpcionesRespuesta
    {

        private Context _context;

        public OpcionesRespuestaRepository()
        {
            this._context = new Context();
        }

        public OpcionesRespuesta addOpcionesRespuesta(OpcionesRespuesta op)
        {
            string exception = String.Empty;

            _context.OpcionesRespuesta.Add(op);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                op = new OpcionesRespuesta() { IdOpcionRTa = -2 };
            }

            return op;
        }
    }
}