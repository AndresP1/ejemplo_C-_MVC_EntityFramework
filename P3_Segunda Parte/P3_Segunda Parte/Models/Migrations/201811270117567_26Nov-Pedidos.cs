namespace P3_Segunda_Parte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26NovPedidos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        PrecioVenta = c.Single(nullable: false),
                        PrecioSugerido = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.LineaPedido",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrecioVenta = c.Single(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Producto_Codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producto", t => t.Producto_Codigo, cascadeDelete: true)
                .Index(t => t.Producto_Codigo);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false, maxLength: 200, unicode: false),
                        Password = c.String(nullable: false),
                        RolCliente = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Mail, unique: true);
            
            CreateTable(
                "dbo.Fabricado",
                c => new
                    {
                        Codigo = c.Int(nullable: false),
                        DiasFabricacion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Producto", t => t.Codigo)
                .Index(t => t.Codigo);
            
            CreateTable(
                "dbo.Importado",
                c => new
                    {
                        Codigo = c.Int(nullable: false),
                        CantMinima = c.Int(nullable: false),
                        PaisOrigen = c.String(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Producto", t => t.Codigo)
                .Index(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Importado", "Codigo", "dbo.Producto");
            DropForeignKey("dbo.Fabricado", "Codigo", "dbo.Producto");
            DropForeignKey("dbo.LineaPedido", "Producto_Codigo", "dbo.Producto");
            DropIndex("dbo.Importado", new[] { "Codigo" });
            DropIndex("dbo.Fabricado", new[] { "Codigo" });
            DropIndex("dbo.Usuario", new[] { "Mail" });
            DropIndex("dbo.LineaPedido", new[] { "Producto_Codigo" });
            DropTable("dbo.Importado");
            DropTable("dbo.Fabricado");
            DropTable("dbo.Usuario");
            DropTable("dbo.LineaPedido");
            DropTable("dbo.Producto");
        }
    }
}
