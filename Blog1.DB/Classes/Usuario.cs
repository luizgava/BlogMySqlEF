using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes.Infra;

namespace Blog1.DB.Classes
{
    public class Usuario : ClasseBase
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
