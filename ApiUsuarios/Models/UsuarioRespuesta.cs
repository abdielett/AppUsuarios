using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUsuarios.Models
{
    public class UsuarioRespuesta
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public List<UsuarioModelo> Resultado { get; set; }
    }
}
