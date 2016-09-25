using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiEncuesta.Models
{
    public class CssControl
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCssControl { get; set; }
        public String Css { get; set; }
        public String Javascript { get; set; }
    }
}