
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

namespace UniversalAPI.Controllers.Repositories
{
    public class ReporteEnlaceRepository : IReporteEnlace
    {

        #region Var Global
        Context _context;
        #endregion

        #region Constructor
        public ReporteEnlaceRepository()
        {
            this._context = new Context();
        }
        #endregion

        #region Public Method

        public ReporteEnlace addReporteEnlace(ReporteEnlace item)
        {
            string exception = String.Empty;

            _context.ReporteEnlace.Add(item);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message : " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                item = new ReporteEnlace() { IdReporteEnlace = -2 };
            }

            return item;
        }

        #endregion

    }
}