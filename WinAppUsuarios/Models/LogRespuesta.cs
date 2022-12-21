using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinAppUsuarios.Models
{
    public class LogRespuesta
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public List<Log> Resultado { get; set; }
    }
}
