using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEncuesta.Models;

namespace WebApiEncuesta.Models.Interfaces
{
    interface ICssControl
    {

       IEnumerable<CssControl> findAllCss();

       dynamic findAllJavascript(int id);

       dynamic findCssXTipoControl(string tipoControl);
    }
}
