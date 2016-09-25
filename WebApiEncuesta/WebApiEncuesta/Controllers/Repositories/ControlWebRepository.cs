using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Models;
using System.Configuration;

namespace WebApiEncuesta.Controllers.Repositories
{
    public class ControlWebRepository : IControlWeb
    {

        #region Variables globales
        Context _context = null;
        #endregion


        public ControlWebRepository()
        {
             _context = new Context();   
        }


        public ControlWeb addControlWeb(ControlWeb cw)
        {
            string exception = String.Empty;

            _context.ControlWeb.Add(cw);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                cw = new ControlWeb() { IdControlWeb = -2 };
            }

            return cw;
        }

        public ControlWeb updateControlWeb(ControlWeb cw)
        {
            string exception = String.Empty;

            var controlWeb = (from cweb in _context.ControlWeb
                              where cweb.IdControlWeb == cw.IdControlWeb
                              select cweb);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                exception = "Exception Message: " + ex.Message + ". Inner Exception: " + ex.InnerException.Message;
                cw = new ControlWeb() { IdControlWeb = -2 };
            }

            return cw;
        }


        public ControlWeb findControlWebXid(int id)
        {
            return _context.ControlWeb.Find(id);
        }


        public IEnumerable<ControlWeb> findAllControlWeb()
        {
            var listControlWeb = (from cw in _context.ControlWeb
                                  select cw).ToList();
            return listControlWeb;
        }


        public dynamic findElementsHtml5TipoElement()
        {
            var listDynamic = (from ControlWebs in _context.ControlWeb
                               select new
                               {
                                   TextField = ControlWebs.TipoElementoHtml5 == null ? ControlWebs.ElementoHtml5 : (ControlWebs.TipoElementoHtml5)
                               }).Distinct().OrderBy(x => x.TextField);
            return listDynamic;
        }


        public ControlWeb deleteControlWeb(int id)
        {
            var controlWeb = new ControlWeb { IdControlWeb = id };
            _context.ControlWeb.Attach(controlWeb);
            _context.ControlWeb.Remove(controlWeb);
            _context.SaveChanges();
            return new ControlWeb { IdControlWeb = 0 };
        }
    }
}