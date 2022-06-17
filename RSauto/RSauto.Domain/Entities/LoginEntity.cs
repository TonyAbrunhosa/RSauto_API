using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Domain.Entities
{
    public class LoginsEntity
    {
        public int ID_USUARIO { get; set; }
        public string NOME_USUARIO { get; set; }
        public string LOGIN_USUARIO { get; set; }
        public string SENHA_USUARIO { get; set; }
    }
}
