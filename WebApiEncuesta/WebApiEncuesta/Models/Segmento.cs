﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class Segmento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSegmento { get; set; }
        public string Nombre { get; set; }
    }
}
