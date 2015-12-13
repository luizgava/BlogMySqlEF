using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog1.DB;
using Blog1.DB.Classes;
using Blog1.Infra;
using Blog1.Models;

namespace Blog1.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            var conexaoBanco = new ConexaoBanco();
            var usuarios = (from p in conexaoBanco.Usuarios
                            orderby p.Nome
                            select p).ToList();

            return View(usuarios);
        }
        #endregion

        #region CadastrarUsuario
        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var usuario = new Usuario();
                usuario.Nome = viewModel.Nome;
                usuario.Login = viewModel.Login.ToUpper();
                usuario.Senha = viewModel.Senha;

                conexaoBanco.Usuarios.Add(usuario);
                try
                {
                    var jaExiste = (from p in conexaoBanco.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe usuário cadastrado com o login {0}.", usuario.Login));
                    }

                    conexaoBanco.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", Erros.Tratar(exp));
                }
            }
            return View(viewModel);
        }
        #endregion

        #region EditarUsuario
        public ActionResult EditarUsuario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var usuario = conexaoBanco.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuário com código {0} não encontrado.", id));
            }
            var viewModel = new UsuarioViewModel
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Nome = usuario.Nome,
                Senha = usuario.Senha
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var usuario = conexaoBanco.Usuarios.FirstOrDefault(x => x.Id == viewModel.Id);
                if (usuario == null)
                {
                    throw new Exception(string.Format("Usuário com código {0} não encontrado.", viewModel.Id));
                }
                usuario.Login = viewModel.Login;
                usuario.Nome = viewModel.Nome;
                if (!string.IsNullOrWhiteSpace(viewModel.Senha))
                {
                    usuario.Senha = viewModel.Senha;
                }
                try
                {
                    var jaExiste = (from p in conexaoBanco.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                       && p.Id != usuario.Id
                                    select p).Any();
                    if (jaExiste)
                    {
                        throw new Exception(string.Format("Já existe usuário cadastrado com o login {0}.", usuario.Login));
                    }

                    conexaoBanco.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            return View(viewModel);
        }
        #endregion

        #region ExcluirUsuario
        public ActionResult ExcluirUsuario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var usuario = (from p in conexaoBanco.Usuarios
                           where p.Id == id
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Usuarios.Remove(usuario);
            conexaoBanco.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}