using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    
    public class Producto
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Debe ser mas explicito en la descripcion.")]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "El valor no debe ser inferior al precio sugerido ni mayor que un 10% de este.")]
        public float PrecioVenta { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "El valor debe ser mayor que 0.")]
        public float PrecioSugerido { get; set; }

        public Producto() { }
    }
}