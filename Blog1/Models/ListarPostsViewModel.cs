using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blog1.DB.Classes;

namespace Blog1.Models
{
    public class ListarPostsViewModel
    {
        public List<DetalhesPostViewModel> Posts { get; set; }
        public List<TagClass> Tags { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public string Pesquisa { get; set; }
        public string Tag { get; set; }
    }

    public class DetalhesPostViewModel
    {
        /*DADOS*/
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public bool Visivel { get; set; }
        public string Resumo { get; set; }
        public int QtdeComentarios { get; set; }
        public string Descricao { get; set; }

        /*TAGS*/
        public IList<TagClass> Tags { get; set; }

        /*CADASTRAR COMENTÁRIO*/
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string ComentarioNome { get; set; }
        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo E-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string ComentarioEmail { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string ComentarioDescricao { get; set; }
        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres!")]
        public string ComentarioPaginaWeb { get; set; }

        /*LISTAR COMENTÁRIOS*/
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IList<Comentario> Comentarios { get; set; }
    }
}