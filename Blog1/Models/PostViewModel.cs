using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog1.Models
{
    public class PostViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [StringLength(100, ErrorMessage = "O campo Título deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Título é obrigatório!")]
        public string Titulo { get; set; }

        [DisplayName("Resumo")]
        [StringLength(1000, ErrorMessage = "O campo Resumo deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Resumo é obrigatório!")]
        public string Resumo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string Descricao { get; set; }

        [DisplayName("Data de publicação")]
        [Required(ErrorMessage = "O campo Data de publicação é obrigatório!")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora de publicação")]
        [Required(ErrorMessage = "O campo Hora de publicação é obrigatório!")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Autor")]
        [StringLength(100, ErrorMessage = "O campo Autor deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Autor é obrigatório!")]
        public string Autor { get; set; }

        [DisplayName("Visível")]
        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }
    }
}