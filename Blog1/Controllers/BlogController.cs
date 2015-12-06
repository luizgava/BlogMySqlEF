using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog1.DB;
using Blog1.Models;

namespace Blog1.Controllers
{
    public class BlogController : Controller
    {
        #region Index
        public ActionResult Index(string tag, string pesquisa, int? pagina)
        {
            var conexao = new ConexaoBanco();
            var posts = from p in conexao.Post
                      select p;

            if (!string.IsNullOrWhiteSpace(tag))
            {
                posts = from p in posts
                       where p.PostTag.Any(x => x.Tag.ToLower() == tag.ToLower())
                      select p;
            }
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                posts = from p in posts
                        where p.Titulo.ToLower().Contains(pesquisa.ToLower())
                        select p;
            }
            pagina = pagina.GetValueOrDefault(1);
            var indicePagina = pagina.Value - 1;
            var itensPorPagina = 10;

            posts = posts.OrderByDescending(x => x.DataPublicacao);

            var viewModel = new ListarPostsViewModel();
            viewModel.Tag = tag;
            viewModel.PaginaAtual = pagina.Value;
            viewModel.Pesquisa = pesquisa;
            viewModel.Posts = posts.Skip(indicePagina*itensPorPagina).Take(itensPorPagina).ToList();
            viewModel.TotalPaginas = (int)Math.Ceiling((double)posts.Count() / itensPorPagina);

            return View(viewModel);
        }
        #endregion

        #region
        public ActionResult _Paginacao()
        {
            return PartialView();
        }
        #endregion
    }
}