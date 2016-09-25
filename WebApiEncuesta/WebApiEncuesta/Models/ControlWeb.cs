using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class ControlWeb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdControlWeb { get; set; }
        public int IdCssControl { get; set; }
        public String ElementoHtml5 { get; set; }
        public String TipoElementoHtml5 { get; set; }
        public string ValorXDefecto { get; set; }
        public Nullable<bool> Requerido { get; set; }
        public String Placeholder { get; set; }
        public Nullable<int> IdPregunta { get; set; }

        [ForeignKey("IdCssControl")]
        public virtual CssControl CssControl { get; set; }

        [ForeignKey("IdPregunta")]
        public virtual Preguntas Preguntas { get; set; }

    }
}