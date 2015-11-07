using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes.Infra;

namespace Blog1.DB.Classes
{
    public class Download : ClasseBase
    {
        public string Ip { get; set; }
        public DateTime DataHora { get; set; }
        public int IdArquivo { get; set; }

        public virtual Arquivo Arquivo { get; set; }
    }
}
