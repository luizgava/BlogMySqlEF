﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog1.DB.Classes.Infra;

namespace Blog1.DB.Classes
{
    public class Arquivo : ClasseBase
    {
        public string Nome { get; set; }
        public string Extensao { get; set; }
        public byte[] Bytes { get; set; }
        public int IdPost { get; set; }

        public virtual Post Post { get; set; }
    }
}
