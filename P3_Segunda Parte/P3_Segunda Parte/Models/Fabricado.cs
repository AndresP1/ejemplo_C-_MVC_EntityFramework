using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    [Table ("Fabricado" ) ]
    public class Fabricado:Producto 
    {
        
        public int DiasFabricacion { get; set; }


        public Fabricado():base()  { }


    }
}