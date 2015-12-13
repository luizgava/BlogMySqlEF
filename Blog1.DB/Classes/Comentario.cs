using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes.Infra;

namespace Blog1.DB.Classes
{
    public class Comentario : ClasseBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string PaginaWeb { get; set; }
        public bool AdmPost { get; set; }
        public int IdPost { get; set; }
        public DateTime DataHora { get; set; }

        public virtual Post Post { get; set; }
    }
}
