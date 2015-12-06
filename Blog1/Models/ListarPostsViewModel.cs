using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog1.DB.Classes;

namespace Blog1.Models
{
    public class ListarPostsViewModel
    {
        public List<Post> Posts { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public string Pesquisa { get; set; }
        public string Tag { get; set; }
    }
}