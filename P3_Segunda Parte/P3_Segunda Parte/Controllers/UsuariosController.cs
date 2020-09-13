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
    public class UsuariosController : Controller
    {
        private EmpresaContext db = new EmpresaContext();

        
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            //if ((string)Session["Tipo"] != null)
            //    return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mail,Password,Confirmacion")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid && usuario.Password == usuario.Confirmacion)
                {
                   var bit= Convert.ToSByte(usuario.RolCliente);
                    bit = 1;
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                return View();

            }

            catch (Exception ex)
            {
                return View();
            }
        }

        
        //get
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Mail, string Password)
        {
            try
            {
                using (EmpresaContext db = new EmpresaContext())
                {
                    var usr = db.Usuarios.SingleOrDefault
                        (u => u.Mail.ToUpper() == Mail.ToUpper()
                        && u.Password == Password);
                    if (usr != null)
                    {
                        Session["Mail"] = usr.Mail;
                        if (usr.RolCliente)
                        {
                            Session["Tipo"] = "Cliente";
                            Session["Carrito"] = new List<LineaPedido>();
                        }
                        else
                            Session["Tipo"] = "Empleado";

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("LoginIncorrecto", "El mail o contraseña no coinciden.");
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Tipo"] = "null";
            Session["Mail"] = "null";
            return RedirectToAction("Login", "Usuarios");
        }
    }


}
