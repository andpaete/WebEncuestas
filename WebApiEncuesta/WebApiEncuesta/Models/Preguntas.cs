using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class Preguntas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPregunta { get; set; }
        public String Nombre { get; set; }
        public int IdEncuesta { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }
        public Nullable<int> IdPreguntaPadre { get; set; }
        [NotMapped]
        public String UsuarioRegistra { get; set; }

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("IdPreguntaPadre")]
        public virtual Preguntas PreguntaPadre { get; set; }

    }
}