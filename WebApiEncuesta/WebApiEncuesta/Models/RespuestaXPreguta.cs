using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{

    public class RespuestaXPreguta
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRespuestaXPregunta { get; set; }
        public int IdPregunta { get; set; }
        public  Nullable<int> IdOpcionRespuesta {get;set;}
        public string TextoNumber { get; set; }
        public string IdUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public Nullable<int> IdReporteEnlaceEncuesta { get; set; }

        [ForeignKey("IdPregunta")]
        public virtual Preguntas Preguntas { get; set; }

        [ForeignKey("IdOpcionRespuesta")]
        public virtual OpcionesRespuesta OpcionesRespuesta { get; set; }
        
    }
}