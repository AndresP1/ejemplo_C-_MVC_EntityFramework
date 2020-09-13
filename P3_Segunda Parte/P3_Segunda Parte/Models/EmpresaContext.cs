using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace P3_Segunda_Parte.Models
{
    public class EmpresaContext:DbContext
    {
        public EmpresaContext() : base("cnAndres")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<LineaPedido> LineaPedidos { get; set; }

        public DbSet<Importado> Importados { get; set; }

        public DbSet<Fabricado> Fabricados { get; set; }

        public DbSet<Producto> Productos { get; set; }

        
    }
}