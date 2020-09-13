using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace P3_Segunda_Parte.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Column(TypeName ="VARCHAR")]
        [StringLength(200, MinimumLength=2)]
        [Required]
        [Index(IsUnique = true/*, ErrorMessage = "El mail ya esta registrado en el sistema."*/)]
        public string Mail { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "La contraseña debe ser mayor a 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //valir al menos una mayus , una minus  y un numero 

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas deben coincidir.")]
        public string Confirmacion { get; set; }


        //0 = empleado , 1 = cliente
        public bool RolCliente { get; set; }

        public Usuario() { }

    }
}