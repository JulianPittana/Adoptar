using Adoptar.Models;
using System.Web.Mvc;

namespace Adoptame.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(string email, string password)
        {
            UsuariosManager manager = new UsuariosManager();
            Usuario usuario = manager.Validar(email, password);
            if (usuario != null)
            {
                //ESTÁ BIEN
                Session["UsuarioLogueado"] = usuario;
            }
            else
            {
                //EL USUARIO NO EXISTE O ESTA MAL LA CONTRASEÑA
                TempData["Error"] = "El usuario no existe o está mal la contraseña";
            }

            return RedirectToAction("Mostrar", "Home");
        }

        public ActionResult Logout()
        {
            Session["UsuarioLogueado"] = null;
            return RedirectToAction("Mostrar", "Home");
        }
        public ActionResult Registrar()
        {
            return View();

        }
        public ActionResult Registrarnuevo(string email, string password, string nombre)
        {
            Usuario Usuarionuevo = new Usuario();
            Usuarionuevo.Mail = email;
            Usuarionuevo.Nombre = nombre;
            Usuarionuevo.Password = password;
            Usuario Usuarioacontrolar = new Usuario();
            Usuarioacontrolar = Usuarionuevo;
            UsuariosManager manager = new UsuariosManager();

//            if(manager.Controlar(Usuarioacontrolar) != null)
//{
//                if (Usuarioacontrolar.Nombre != null)
//                {
//                    Session["controlnombre"] = "usado";
//                }
//                if (Usuarioacontrolar.Mail != null)
//                {
//                    Session["controlmail"] = "usado";
//                }

//                return RedirectToAction ("Registrar", "Usuarios");
//            }


            manager.Registrarse(Usuarionuevo);
            return View();

        }
    }
}