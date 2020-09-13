using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    [Table("Importado")]
    public class Importado : Producto
    {
        
        public int CantMinima { get; set; }

        public string PaisOrigen { get; set; }

        public Importado(){ }
    }
}