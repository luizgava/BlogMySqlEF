using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog1.Models
{
    public class UsuarioViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Login")]
        [StringLength(30, ErrorMessage = "O campo Login deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Login é obrigatório!")]
        public string Login { get; set; }

        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [DisplayName("Senha")]
        [StringLength(100, ErrorMessage = "O campo Senha deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Senha { get; set; }

        [DisplayName("Confirmar senha")]
        [StringLength(100, ErrorMessage = "O campo Senha deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem.")]
        public string ConfirmarSenha { get; set; }
    }
}