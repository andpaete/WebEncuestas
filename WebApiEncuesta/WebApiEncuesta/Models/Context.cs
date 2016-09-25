using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("Connection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ControlWeb> ControlWeb { get; set; }
        public DbSet<CssControl> CssControl { get; set; }
        public DbSet<Encuesta> Encuesta { get; set; }
        public DbSet<EncuestaXReporteEnlace> EncuestaXReporteEnlace { get; set; }
        public DbSet<OpcionesRespuesta> OpcionesRespuesta { get; set; }        
        public DbSet<Preguntas> Preguntas { get; set; }
        public DbSet<ReporteEnlace> ReporteEnlace { get; set; }
        public DbSet<RespuestaXPreguta> RespuestaXPreguta { get; set; }
        public DbSet<Segmento> Segmento { get; set; }
    }
}
