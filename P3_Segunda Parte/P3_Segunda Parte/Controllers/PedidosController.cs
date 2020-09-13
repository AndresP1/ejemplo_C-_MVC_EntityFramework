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
    public class PedidosController : Controller
    {
        private EmpresaContext db = new EmpresaContext();

        // GET: Pedidos
        public ActionResult Index()
        {

            if ((string)Session["Tipo"] != "Cliente")
                return RedirectToAction("Index", "Home");

            string mail = Session["Mail"] as string;
            List<Pedido> pedidos = db.Pedidos.Where(p=>p.Usuario.Mail==mail).ToList();
            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {   
            if ((string)Session["Tipo"] != "Cliente")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<LineaPedido> pedidos = db.LineaPedidos.Where(p=>p.Pedido.id==id).ToList();
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
