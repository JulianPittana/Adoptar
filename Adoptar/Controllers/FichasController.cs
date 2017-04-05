using Adoptar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Adoptame.Controllers
{
    public class FichasController : Controller
    {

        public ActionResult Agregar()
        {

            return View();

        }

        public ActionResult Mostrar()
        {
            FichasManager manager = new FichasManager();
            List<Ficha> fichas = manager.ConsultarTodos();
            ViewBag.Fichas = fichas;


            return View();

        }

        public ActionResult Buscar(Ficha ficha)
        {


            return View();
        }
        public ActionResult Buscarfichas(Ficha ficha)
        {
            FichasManager manager = new FichasManager();
            List<Ficha> fichas = manager.Buscar(ficha);
            ViewBag.Fichas = fichas;

            return View();

        }
        public ActionResult Mismascotas()
        {
            FichasManager manager = new FichasManager();
            Usuario usuario = (Usuario)Session["UsuarioLogueado"];
            List<Ficha> fichas = manager.ConsultarMios(usuario);
            ViewBag.Fichas = fichas;

            return View();

        }

        public ActionResult MostrarUno(long id)
        {
            FichasManager manager = new FichasManager();
            Ficha ficha = manager.Consultar(id);
            ViewBag.ficha = ficha;
            return View();

        }

        public ActionResult Adoptar(string nombre, string ubicacion, string telefono, string mail, string texto, long id)
        {
            FichasManager manager = new FichasManager();
            Ficha ficha = manager.Consultar(id);
            ViewBag.ficha = ficha;

            string mensaje = "";
            try
            {
                var smptc = new SmtpClient();
                smptc.Host = "Smtp.Gmail.com";
                smptc.Port = 587;
                smptc.EnableSsl = true;
                smptc.Timeout = 10000;
                smptc.DeliveryMethod = SmtpDeliveryMethod.Network;
                smptc.UseDefaultCredentials = false;
                smptc.Credentials = new NetworkCredential("julian.pittana@gmail.com", "Horadrim5");

                var message = new MailMessage();
                message.From = new MailAddress("julian.pittana@gmail.com", "Julian");
                message.To.Add(new MailAddress(mail));
                message.Subject = nombre + " quiere adoptar a " + ficha.Nombre;
                message.Body = texto;
                message.BodyEncoding = Encoding.UTF8;
                smptc.Send(message);
                //Si todo sale bien configuro una mensaje
                mensaje = "Email has been sent successfully.";
            }
            catch (Exception ex)
            {
                mensaje = "ERROR: " + ex.Message;
            }
            ViewBag.mensaje = mensaje;
            return View("/Views/Fichas/Adoptarmensaje.cshtml");
        }

        public ActionResult Modificar(long id)
        {
            FichasManager manager = new FichasManager();
            Ficha ficha = manager.Consultar(id);
            ViewBag.ficha = ficha;
            return View();
        }

        public ActionResult ModificarFicha(Ficha ficha)
        {
            FichasManager manager = new FichasManager();
            manager.Modificar(ficha);
            return RedirectToAction("MostrarUno", "Fichas", new { id = @ficha.ID });
        }

        public ActionResult Eliminar(long id)
        {
            FichasManager manager = new FichasManager();
            manager.Eliminar(id);
            return View();
        }

        // GET: Fichas

        [HttpPost]
        public ActionResult GuardarFicha(string tipo, string nombre, string imagen, string texto, string sexo, string ubicacion, string edad)
        {
            Ficha nuevaFicha = new Ficha();
            nuevaFicha.Tipo = tipo;
            nuevaFicha.Nombre = nombre;
            nuevaFicha.Imagen = imagen;
            nuevaFicha.Texto = texto;
            nuevaFicha.Sexo = sexo;
            nuevaFicha.Ubicacion = ubicacion;
            nuevaFicha.Edad = edad;
            nuevaFicha.Autor = ((Adoptar.Models.Usuario)Session["UsuarioLogueado"]);
            FichasManager manager = new FichasManager();
            manager.Insertar(nuevaFicha);

            return RedirectToAction("Mostrar", "Fichas");
        }
    }
}