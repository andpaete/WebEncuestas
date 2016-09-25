
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
using WebApiEncuesta.Models;
using WebApiEncuesta.Models.Interfaces;
using System.Web.Hosting;

namespace WebApiEncuesta.Controllers.Repositories
{
    public class EncuestaXReporteEnlaceRepository  : IEncuestaXReporteEnlace
    {

        #region Var global
        Context _context;
        #endregion

        #region constructor


        public EncuestaXReporteEnlaceRepository()
        {
            this._context = new Context();
        }

        #endregion

        #region public Method

       
        public EncuestaXReporteEnlace addEncuestaXReporteEnlace(EncuestaXReporteEnlace item)
        {
            string exception = String.Empty;
            DateTime fecha = DateTime.Now;

            item.FechaInicial = Convert.ToDateTime(item.FechaInicialString); 

            var report = (from re in _context.ReporteEnlace
                           where re.NombreTable == item.NombreTable
                           select new
                           {
                               re.IdReporteEnlace
                           }
                           ).FirstOrDefault();

           

            item.IdReporteEnlace = report.IdReporteEnlace;
            item.FechaFin = fecha;

            _context.EncuestaXReporteEnlace.Add(item);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                item = new EncuestaXReporteEnlace() { IdEncuestaXReporteEnlace = -2 };
            }
            

            return item;
        }

        #endregion

    }
}