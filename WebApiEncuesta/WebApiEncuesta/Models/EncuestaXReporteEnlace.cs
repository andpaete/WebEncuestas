using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{

    public class EncuestaXReporteEnlace
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEncuestaXReporteEnlace { get; set; }
        public int IdEncuesta { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdReporteEnlace { get; set; }
        public int IdCampo { get; set; }

        [NotMapped]
        public string FechaInicialString { get; set; }

        [NotMapped]
        public string NombreTable { get; set; }

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("IdReporteEnlace")]
        public virtual ReporteEnlace ReporteEnlace { get; set; }

    }
}