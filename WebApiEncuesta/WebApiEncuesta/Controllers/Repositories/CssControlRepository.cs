using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiEncuesta.Models.Interfaces;
using WebApiEncuesta.Models;

namespace WebApiEncuesta.Controllers.Repositories
{
    public class CssControlRepository   : ICssControl
    {

        private Context _contex;

        public CssControlRepository()
        {
            this._contex = new Context();
        }


        public IEnumerable<CssControl> findAllCss()
        {
            var listCss = (from css in _contex.CssControl
                           select css).Distinct().ToList();
            return listCss;
        }

        public dynamic findAllJavascript(int id)
        {
            var listDynamic = (from css in _contex.CssControl
                               where css.Javascript != null  &&
                                     css.IdCssControl == id
                               select css).Distinct().ToList();
            return listDynamic;
        }

        public dynamic findCssXTipoControl(string tipoControl)
        {
            var listCss = (from css in _contex.ControlWeb
                               where css.TipoElementoHtml5.Contains(tipoControl)
                               select new
                               {
                                   css.CssControl.Css,
                                   css.CssControl.IdCssControl
                               }
                         ).Distinct().ToList();
            if(listCss.Count() > 0 )
                return listCss;
            else
                return (from css in _contex.ControlWeb
                        where css.ElementoHtml5.Contains(tipoControl)
                        select new
                        {
                            css.CssControl.Css,
                            css.CssControl.IdCssControl
                        }
                         ).Distinct().ToList();
        }
    }
}