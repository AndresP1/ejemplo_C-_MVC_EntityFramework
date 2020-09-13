using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using P3_Segunda_Parte.Models;

namespace P3_Segunda_Parte.Controllers
{
    public class LineaPedidosController : Controller
    {
        private EmpresaContext db = new EmpresaContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            if ((string)Session["Tipo"] != "Cliente")
                return RedirectToAction("Index", "Home");
            return View();
        }


        public ActionResult VerCarrito()
        {

            if ((string)Session["Tipo"] != "Cliente")
                return RedirectToAction("Index", "Home");

            var carrito = (List<LineaPedido>)Session["Carrito"];
            if (carrito != null)
                return View(carrito.ToList());
            else
            {

                return View(new List<LineaPedido>());
            }
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaPedido pedido = db.LineaPedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PrecioVenta,Cantidad,Fecha")] LineaPedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.LineaPedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaPedido pedido = db.LineaPedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PrecioVenta,Cantidad,Fecha")] LineaPedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaPedido pedido = db.LineaPedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaPedido pedido = db.LineaPedidos.Find(id);
            db.LineaPedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Agregar1(int id)
        {

            var lista = Session["Carrito"] as List<LineaPedido>;
            bool encontrado = false;
            int contador = 0;
            while (!encontrado && contador < lista.Count)
            {
                if (lista[contador].Id == id)
                {
                    encontrado = true;
                    lista[contador].Cantidad++;
                }

                contador++;
            }
            Session["Carrito"] = lista;
            return RedirectToAction("VerCarrito");
        }

        public ActionResult Restar1(int id)
        {

            var lista = Session["Carrito"] as List<LineaPedido>;
            bool encontrado = false;
            int contador = 0;
            while (!encontrado && contador < lista.Count)
            {
                if (lista[contador].Id == id && lista[contador].Cantidad > 1)
                {
                    lista[contador].Cantidad--;
                    encontrado = true;
                }

                contador++;
            }
            Session["Carrito"] = lista;
            return RedirectToAction("VerCarrito");
        }

        public ActionResult Eliminar(int id)
        {

            var lista = Session["Carrito"] as List<LineaPedido>;
            LineaPedido encontrado = null;
            int contador = 0;
            
            while (encontrado == null && contador < lista.Count)
            {
                if (lista[contador].Id == id )
                {
                    encontrado = lista[contador];
                }

                contador++;
            }
            if (encontrado != null) {
                lista.Remove(encontrado);
                TempData["MensajeEliminado"] = $"{encontrado.Producto.Nombre}  eliminado.";
            }
            Session["Carrito"] = lista;
            return RedirectToAction("VerCarrito");
        }

        public ActionResult FinalizarCompra()
        {
            EmpresaContext db = new EmpresaContext();

            var carrito = (List<LineaPedido>)Session["Carrito"];
            double total = 0;
            string sesionmail = (string)Session["Mail"];

            Pedido pedido = new Pedido();
            pedido.Fecha = DateTime.Now;
            pedido.Usuario = db.Usuarios.FirstOrDefault(u => u.Mail == sesionmail);
            pedido.Pedidos = new List<LineaPedido>();

            foreach (LineaPedido p in carrito)
            {
                if (p.Cantidad < 0)
                {
                    return View(carrito.ToList());
                }
                if (p.Cantidad > 0)
                {
                    pedido.Pedidos.Add(p);
                    total += p.Cantidad*p.PrecioVenta;
                }
            }

            db.Pedidos.Add(pedido);
            db.SaveChanges();

            Session["Carrito"] = new List<LineaPedido>();
            TempData["montoCarrito"] = "Total de la compra:" + "$" + total;

            TempData["ur"] = pedido.Usuario.Mail;
            TempData["fecha"] = pedido.Fecha;

            return View(pedido.Pedidos);

        }

    }
}
