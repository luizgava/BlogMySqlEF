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
            viewModel.Posts = (from p in posts.Skip(indicePagina*itensPorPagina).Take(itensPorPagina)
                            select new DetalhesPostViewModel
                            {
                                Id = p.Id,
                                Autor = p.Autor,
                                DataPublicacao = p.DataPublicacao,
                                Titulo = p.Titulo,
                                Resumo = p.Resumo,
                                Visivel = p.Visivel,
                                QtdeComentarios = p.Comentarios.Count()
                            }).ToList();
            viewModel.Tags = (from p in conexao.Tags
                             where conexao.PostTags.Any(x => x.Tag == p.Tag)
                            select p).ToList();
            viewModel.TotalPaginas = (int)Math.Ceiling((double)posts.Count() / itensPorPagina);

            return View(viewModel);
        }
        #endregion

        #region _Paginacao
        public ActionResult _Paginacao()
        {
            return PartialView();
        }
        #endregion

        #region Post
        public ActionResult Post(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Post
                        where p.Id == id
                        select new DetalhesPostViewModel
                        {
                            Id = p.Id,
                            Autor = p.Autor,
                            DataPublicacao = p.DataPublicacao,
                            Titulo = p.Titulo,
                            Resumo = p.Resumo,
                            Visivel = p.Visivel,
                            QtdeComentarios = p.Comentarios.Count,
                            Descricao = p.Descricao,
                            Tags = p.PostTag.Select(x => x.TagClass).ToList()
                        }).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado.", id));
            }

            return View(post);
        }
        #endregion
    }
}