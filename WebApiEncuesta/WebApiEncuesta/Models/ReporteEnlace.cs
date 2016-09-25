using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{

    public class ReporteEnlace
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReporteEnlace { get; set; }
        public string NombreTable { get; set; }
        public string NombreCampo { get; set; }
    }
}