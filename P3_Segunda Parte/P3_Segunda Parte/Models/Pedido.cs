using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    public class Pedido
    {
        public int id { get; set; }

        public List<LineaPedido> Pedidos{ get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Usuario Usuario { get; set; }

        public Pedido() { }
    }
}