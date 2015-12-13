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
    public class AdministracaoController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region CadastrarPost
        public ActionResult CadastrarPost()
        {
            var viewModel = new PostViewModel
            {
                DataPublicacao = DateTime.Now,
                HoraPublicacao = DateTime.Now,
                Visivel = true,
                Tags = new List<string>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var post = new Post();
                post.DataPublicacao = new DateTime(viewModel.DataPublicacao.Year, viewModel.DataPublicacao.Month, viewModel.DataPublicacao.Day, viewModel.HoraPublicacao.Hour, viewModel.HoraPublicacao.Minute, 0);
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                post.Titulo = viewModel.Titulo;
                post.Visivel = viewModel.Visivel;
                post.PostTag = new List<PostTag>();
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tag = conexaoBanco.Tags.FirstOrDefault(x => x.Tag.ToLower() == item.ToLower());
                        if (tag == null)
                        {
                            tag = new TagClass
                            {
                                Tag = item
                            };
                            conexaoBanco.Tags.Add(tag);
                        }
                        post.PostTag.Add(new PostTag
                        {
                            Tag = item,
                            TagClass = tag
                        });
                    }
                }

                conexaoBanco.Post.Add(post);

                try
                {
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

        #region EditarPost
        public ActionResult EditarPost(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = conexaoBanco.Post.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }
            var viewModel = new PostViewModel
            {
                DataPublicacao = post.DataPublicacao,
                HoraPublicacao = post.DataPublicacao,
                Titulo = post.Titulo,
                Visivel = post.Visivel,
                Resumo = post.Resumo,
                Autor = post.Autor,
                Descricao = post.Descricao,
                Id = post.Id,
                Tags = post.PostTag.Select(x => x.Tag).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarPost(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexaoBanco = new ConexaoBanco();
                var post = conexaoBanco.Post.FirstOrDefault(x => x.Id == viewModel.Id);
                if (post == null)
                {
                    throw new Exception(string.Format("Post com código {0} não encontrado.", viewModel.Id));
                }
                post.DataPublicacao = new DateTime(viewModel.DataPublicacao.Year, viewModel.DataPublicacao.Month, viewModel.DataPublicacao.Day, viewModel.HoraPublicacao.Hour, viewModel.HoraPublicacao.Minute, 0);
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                post.Titulo = viewModel.Titulo;
                post.Visivel = viewModel.Visivel;
                var postsTagsAtuais = post.PostTag.ToList();
                foreach (var item in postsTagsAtuais)
                {
                    conexaoBanco.PostTags.Remove(item);
                }
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tag = conexaoBanco.Tags.FirstOrDefault(x => x.Tag.ToLower() == item.ToLower());
                        if (tag == null)
                        {
                            tag = new TagClass
                            {
                                Tag = item
                            };
                            conexaoBanco.Tags.Add(tag);
                        }
                        post.PostTag.Add(new PostTag
                        {
                            Tag = item,
                            TagClass = tag
                        });
                    }
                }

                try
                {
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

        #region ExcluirPost
        public ActionResult ExcluirPost(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Post
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não foi encontrado.", id));
            }
            conexaoBanco.Post.Remove(post);
            conexaoBanco.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                             where p.Id == id
                           select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Post
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion
    }
}