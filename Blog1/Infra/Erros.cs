using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog1.Infra
{
    public static class Erros
    {
        public static string Tratar(Exception exp)
        {
            var inner = exp;
            while (inner.InnerException != null)
            {
                inner = inner.InnerException;
            }
            return inner.Message;
        }
    }
}