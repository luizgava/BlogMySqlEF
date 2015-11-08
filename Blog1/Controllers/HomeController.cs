using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog1.DB;
using Blog1.DB.Classes;

namespace Blog1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var conexao = new ConexaoBanco();
            var usuarioUltimo = new Usuario
            {
                Login = "LUIZ",
                Senha = "luiz",
                Nome = "Luiz Otávio Bortolotto Gava"
            };
            conexao.Usuarios.Add(usuarioUltimo);
            conexao.SaveChanges();
            
            usuarioUltimo.Nome += " alterado";
            conexao.SaveChanges();
            
            conexao.Usuarios.Remove(usuarioUltimo);
            conexao.SaveChanges();

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}