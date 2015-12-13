using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog1.DB;
using Blog1.Models;

namespace Blog1.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        #region Index
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var conexaoBanco = new ConexaoBanco();
            var usuario = (from p in conexaoBanco.Usuarios
                          where p.Login.ToUpper() == viewModel.Login.ToUpper()
                         select p).FirstOrDefault();
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
                return View(viewModel);
            }
            FormsAuthentication.SetAuthCookie(usuario.Login, viewModel.Lembrar);
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region Sair

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion
    }
}