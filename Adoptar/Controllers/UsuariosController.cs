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

            return RedirectToAction("Mostrar", "Fichas");
        }

        public ActionResult Logout()
        {
            Session["UsuarioLogueado"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Registrar()
        {
            return View();

        }
        public ActionResult Registrarnuevo()
        {


            return View();

        }
    }
}