using System;
using System.Collections.Generic;
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
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public bool Visivel { get; set; }
        public string Resumo { get; set; }
        public int QtdeComentarios { get; set; }
        public string Descricao { get; set; }
        public IList<TagClass> Tags { get; set; }
    }
}