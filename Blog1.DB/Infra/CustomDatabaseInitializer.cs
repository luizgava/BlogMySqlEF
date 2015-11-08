using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes;

namespace Blog1.DB.Infra
{
    public class CustomDatabaseInitializer<T> : DropCreateDatabaseIfModelChanges<ConexaoBanco>
    {
        protected override void Seed(ConexaoBanco dbDataContext)
        {
            dbDataContext.Usuarios.Add(new Usuario { Login = "ADM", Senha = "adm", Nome = "Administrador" });

            base.Seed(dbDataContext);
        }
    }
}