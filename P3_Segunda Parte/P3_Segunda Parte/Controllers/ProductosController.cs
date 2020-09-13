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
    public class ProductosController : Controller
    {
        private EmpresaContext db = new EmpresaContext();

        // GET: Productos
        /*public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }*/
        public ActionResult Index()
        {
            if ((string)Session["Tipo"] == "Cliente")
                return RedirectToAction("Listado");


            List<Producto> lista = db.Productos.ToList();


            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(int? cod, string nom, string desc, int? desde, int? hasta)
        {
            if ((string)Session["Tipo"] == "Cliente")
                return RedirectToAction("Listado");

           List<Producto> lista = db.Productos.ToList();
            //  List<Producto> lista = db.Productos.Where(o=>o.PrecioVenta==100).OrderBy(o => o.Codigo).Take(3).ToList();


            if (cod != null && cod > 0)
                lista = lista.Where(p => p.Codigo == cod).ToList();
            if (nom != "")
                lista = lista.Where(p => p.Nombre.Contains(nom)).ToList();
            if (desc != "")
                lista = lista.Where(p => p.Descripcion.Contains(desc)).ToList();
            if (desde != null && desde > 0)
                lista = lista.Where(p => p.PrecioVenta >= desde).ToList();
            if (hasta != null && hasta > 1)
                lista = lista.Where(p => p.PrecioVenta <= hasta).ToList();

            
            return View(lista);
        }

        public ActionResult Listado()
        {
            if ((string)Session["Tipo"] == "Empleado")
                return RedirectToAction("Index");


            List<Producto> lista = db.Productos.ToList();

            return View(lista);
        }

        [HttpPost]
        public ActionResult Listado(int? cod, string nom,string desc,int? desde,int? hasta) {


            if ((string)Session["Tipo"] == "Empleado")
                return RedirectToAction("Index");


            List<Producto> lista = db.Productos.ToList();

            
            if (cod != null && cod > 0)
                lista= lista.Where(p => p.Codigo == cod).ToList();
            if (nom != "")
                lista = lista.Where(p => p.Nombre.Contains(nom)).ToList();
            if (desc != "")
                lista = lista.Where(p => p.Descripcion.Contains(desc)).ToList();
            if (desde != null && desde > 0)
                lista = lista.Where(p => p.PrecioVenta >= desde).ToList();
            if (hasta != null && hasta > 1)
                lista = lista.Where(p => p.PrecioVenta <= hasta).ToList();
            return View(lista);
        }
        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Descripcion,PrecioVenta,PrecioSugerido")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        //GET
        public ActionResult AsignarPrecio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarPrecio([Bind(Include = "Codigo,Nombre,Descripcion,PrecioVenta,PrecioSugerido")] Producto producto)
        {
            //if ((string)Session["Tipo"] == "Empleado")
            //{

            if (ModelState.IsValid && producto.PrecioVenta >= producto.PrecioSugerido && producto.PrecioVenta <= (producto.PrecioSugerido * 1.10))
            {

                if (ModelState.IsValid)
                {
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            //}
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Descripcion,PrecioVenta,PrecioSugerido")] Producto producto)
        {
            //if ((string)Session["Tipo"] == "Empleado")
            //{

              if (ModelState.IsValid && producto.PrecioVenta >= producto.PrecioSugerido && producto.PrecioVenta <= (producto.PrecioSugerido * 1.10))
              {

                    if (ModelState.IsValid)
                    {
                        db.Entry(producto).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
              }
                
            //}
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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

        public ActionResult AgregarAlCarrito(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            LineaPedido p = new LineaPedido();
            p.Cantidad = 1;
            
            p.Producto=producto;
            p.PrecioVenta = producto.PrecioVenta;
            TempData["MensajeCarrito"] = producto.Nombre + " $ " + producto.PrecioVenta;

            var carrito = (List<LineaPedido>)Session["Carrito"];
            carrito.Add(p);
            Session["Carrito"] = carrito;


            return RedirectToAction("Listado");
        }

       
    }
}
