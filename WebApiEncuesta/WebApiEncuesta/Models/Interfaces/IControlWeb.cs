using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEncuesta.Models;

namespace WebApiEncuesta.Models.Interfaces
{
    interface IControlWeb
    {

       /// <summary>
        /// crear un ControlWeb.
       /// </summary>
        /// <param name="cw" type="ControlWeb">Object ControlWeb to create</param>
        /// <returns>ControlWeb</returns>
        ControlWeb addControlWeb(ControlWeb cw);
        
        /// <summary>
        /// Modificar un ControlWeb.
        /// </summary>
        /// <param name="cw" type="ControlWeb">Object ControlWeb to update</param>
        /// <returns>ControlWeb</returns>
        ControlWeb updateControlWeb(ControlWeb cw);

        /// <summary>
        /// Buscar un ControlWeb por el id
        /// </summary>
        /// <param name="id" type="int">Id a buscar</param>
        /// <returns>ControlWeb</returns>
        ControlWeb findControlWebXid(int id);

        /// <summary>
        /// Busca todos los controles web.
        /// </summary>
        /// <returns>Lista de ControlWeb<ControlWeb></returns>
        IEnumerable<ControlWeb> findAllControlWeb();

        /// <summary>
        /// Buscar todos  los elementos html 5 con su respectivo tipo
        /// </summary>
        /// <returns>Lista dinamica</returns>
        dynamic findElementsHtml5TipoElement();

        /// <summary>
        /// Eliminar un control web por el id
        /// </summary>
        /// <param name="id">Id del control a borrar</param>
        /// <returns>null</returns>
        ControlWeb deleteControlWeb(int id);
    }
}
