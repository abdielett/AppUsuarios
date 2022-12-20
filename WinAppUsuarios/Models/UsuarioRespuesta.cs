using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppUsuarios.Models
{
    class UsuarioRespuesta
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public List<Usuario> Resultado { get; set; }
    }
}
