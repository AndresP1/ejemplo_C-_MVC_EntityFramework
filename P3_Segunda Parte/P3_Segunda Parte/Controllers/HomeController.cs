using P3_Segunda_Parte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;





namespace P3_Segunda_Parte.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult CargarProductos()
        {
            EmpresaContext emp = new EmpresaContext();
            if ((string)Session["Tipo"] == "Empleado")
            {
                if (emp.Productos.Count() == 0)
                {
                    System.IO.StreamReader sr = null;

                    try
                    {
                        sr = System.IO.File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "Productos.txt");
                    }
                    catch (Exception)
                    {

                    }

                    if (sr != null)
                    {

                        using (EmpresaContext db = new EmpresaContext())
                        {
                            bool quedanLineas = true;
                            while (quedanLineas)
                            {
                                string linea = sr.ReadLine();
                                if (linea == null)
                                {
                                    quedanLineas = false;
                                }
                                else
                                {
                                    string[] claveValor = linea.Split('|');
                                    int codigo = Convert.ToInt32(claveValor[1]);
                                    if (db.Productos.SingleOrDefault(p => p.Codigo == codigo) == null)
                                    {
                                        if (claveValor[0] == "Importacion")
                                        {
                                            Importado i = new Importado();
                                            i.Nombre = claveValor[2];
                                            i.Descripcion = claveValor[3];
                                            i.PrecioVenta = Convert.ToInt32(claveValor[5]);
                                            i.PrecioSugerido = Convert.ToInt32(claveValor[5]);
                                            i.CantMinima = Convert.ToInt32(claveValor[9]);
                                            i.PaisOrigen = claveValor[8];

                                            db.Importados.Add(i);
                                        }
                                        else
                                        {
                                            Fabricado i = new Fabricado();
                                            i.Nombre = claveValor[2];
                                            i.Descripcion = claveValor[3];
                                            i.PrecioVenta = Convert.ToInt32(claveValor[5]);
                                            i.PrecioSugerido = Convert.ToInt32(claveValor[5]);
                                            i.DiasFabricacion = Convert.ToInt32(claveValor[7]);

                                            db.Fabricados.Add(i);
                                        }

                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                    return RedirectToAction("Index", "Productos");
                }

                else
                {
                    TempData["MensajeCargaProductos"] = "Los productos ya estan cargados en la base.";
                    return RedirectToAction("Index","Home");
                }
            }
            else
                if ((string)Session["Tipo"] == "Cliente")
                return RedirectToAction("Listado", "Productos");
            else
                return
                    RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Index()
        {
            if ((string)Session["Tipo"] == null)
                return RedirectToAction("Login", "Usuarios");
            return View();
        }
        public ActionResult btnProductos()
        {
            if ((string)Session["Tipo"] == "Cliente")
                return RedirectToAction("Listado", "Productos");

            if ((string)Session["Tipo"] == "Empleado")
                return RedirectToAction("Index", "Productos");

            return RedirectToAction("Login", "Usuarios");
        }
    }
}