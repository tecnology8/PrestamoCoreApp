using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoCoreApp.Models
{
    public class PrestamoModel
    {
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Tasa { get; set; }
    }
}
