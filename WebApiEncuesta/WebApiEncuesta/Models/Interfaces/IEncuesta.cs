using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEncuesta.Models;

namespace WebApiEncuesta.Models.Interfaces
{
    interface IEncuesta
    {
        Encuesta addEncuesta(Encuesta encuesta);

        Encuesta updateEncuesta(Encuesta encuesta);

        Encuesta findEncuestaXNombre(String nombre, int idSegmento);

        Encuesta findEncuestaXId(int id);

        IEnumerable<Encuesta> findAllEncuestas();

        IEnumerable<Encuesta> findAllEncuestasXEstado(bool estado);

        bool findEncuestaXNombreBool(string nombre, int idSegmento);

        dynamic findAllEncuestasListDynamic(int idsegmento);

        dynamic findEncuentaPreguntaXSegId(int idSegmento, int idEncuesta);

        dynamic findEncustasXSegmentoEstado(int idSegmento);

        dynamic findPollXPregunta(int idPregunta);

        dynamic ConsultaRespuestaNo(int idProyecto, int Id, string OpRespuesta, int idEncuesta); 

    }
}
