using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    public class LineaPedido
    {
        public int Id { get; set; }

        [Required]
        public Pedido Pedido { get; set; }

        [Required]
        public Producto Producto { get; set; }

        [Required]
        public float PrecioVenta { get; set; }

        [Required]
        public int Cantidad { get; set; }

        
        public LineaPedido()  { }

    }
}