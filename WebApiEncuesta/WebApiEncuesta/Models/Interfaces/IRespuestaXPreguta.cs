using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEncuesta.Models.Interfaces
{

    interface IRespuestaXPreguta
    {

        RespuestaXPreguta addRespuestaXPreguta(RespuestaXPreguta rxp);

        bool addListRespuestaXPreguta(List<RespuestaXPreguta> listRespuestaXPreguta);

        dynamic getEncuestasXProyecto(int idCampo);
    }
}
