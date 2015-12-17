using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog1.DB;
using Blog1.DB.Classes;
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
            viewModel.Posts = (from p in posts.Skip(indicePagina * itensPorPagina).Take(itensPorPagina)
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
        public ActionResult Post(int id, int? pagina)
        {
            var conexaoBanco = new ConexaoBanco();
            var viewModel = new DetalhesPostViewModel();
            viewModel = preparaDetalhesPostViewModel(id, pagina, viewModel, conexaoBanco);

            return View(viewModel);
        }

        private static DetalhesPostViewModel preparaDetalhesPostViewModel(int id, int? pagina, DetalhesPostViewModel viewModel, ConexaoBanco conexaoBanco)
        {
            var post = (from p in conexaoBanco.Post
                where p.Id == id
                select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não encontrado.", id));
            }
            viewModel.Id = post.Id;
            viewModel.Autor = post.Autor;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.Titulo = post.Titulo;
            viewModel.Resumo = post.Resumo;
            viewModel.Visivel = post.Visivel;
            viewModel.QtdeComentarios = post.Comentarios.Count;
            viewModel.Descricao = post.Descricao;
            viewModel.Tags = post.PostTag.Select(x => x.TagClass).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var indicePagina = paginaCorreta - 1;
            var itensPorPagina = 10;

            var comentarios = from p in conexaoBanco.Comentarios
                where p.IdPost == id
                orderby p.DataHora descending
                select p;

            viewModel.PaginaAtual = paginaCorreta;
            viewModel.Comentarios = (from p in comentarios.Skip(indicePagina*itensPorPagina).Take(itensPorPagina)
                                   select p).ToList();
            viewModel.TotalPaginas = (int) Math.Ceiling((double) comentarios.Count()/itensPorPagina);
            return viewModel;
        }

        [HttpPost]
        public ActionResult Post(DetalhesPostViewModel viewModel)
        {
            var conexaoBanco = new ConexaoBanco();
            if (ModelState.IsValid)
            {
                var comentario = new Comentario();
                comentario.AdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.Descricao = viewModel.ComentarioDescricao;
                comentario.DataHora = DateTime.Now;
                comentario.Email = viewModel.ComentarioEmail;
                comentario.IdPost = viewModel.Id;
                comentario.Nome = viewModel.ComentarioNome;
                comentario.PaginaWeb = viewModel.ComentarioPaginaWeb;
                conexaoBanco.Comentarios.Add(comentario);

                try
                {
                    conexaoBanco.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = viewModel.DataPublicacao.Year,
                        mes = viewModel.DataPublicacao.Month,
                        dia = viewModel.DataPublicacao.Day,
                        titulo = viewModel.Titulo,
                        id = viewModel.Id
                    })+"#comentarios");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                }
            }
            preparaDetalhesPostViewModel(viewModel.Id, viewModel.PaginaAtual, viewModel, conexaoBanco);
            return View(viewModel);
        }
        #endregion

        #region _PaginacaoPost
        public ActionResult _PaginacaoPost()
        {
            return PartialView();
        }
        #endregion
    }
}