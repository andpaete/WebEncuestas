using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class OpcionesRespuesta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOpcionRTa { get; set; }
        public String Nombre { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }
        public int IdControlWeb { get; set; }
        public Nullable<int> IdEncuestareload { get; set; }

        [ForeignKey("IdControlWeb")]
        public virtual ControlWeb ControlWeb { get; set; }

    }
}