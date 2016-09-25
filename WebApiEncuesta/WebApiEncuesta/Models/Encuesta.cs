using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class Encuesta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEncuesta { get; set; }
        public bool Estado { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdSegmento { get; set; }
        public Nullable<int> IdEncuestaRedirec { get; set; }

        [NotMapped]
        public String UsuarioRegistra { get; set; }

        [ForeignKey("IdSegmento")]
        public virtual Segmento Segmento { get; set; }
    }
}