using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEncuesta.Models;

namespace WebApiEncuesta.Models.Interfaces
{
    interface IPreguntas
    {

        Preguntas addPregunta(Preguntas p);

        Preguntas updatePregunta(Preguntas p);

        Preguntas findPreguntaXid(int id);

        IEnumerable<Preguntas> findAllPreguntas();

        dynamic findPreguntasXEncuesta(int idEncuesta);

        Preguntas findPreguntaXNombre(string nombre,int idEncuesta);

        dynamic findPreguntasToDataTable(int idEncuesta);

        bool deletePrgunta(int idPregunta);
    }
}
