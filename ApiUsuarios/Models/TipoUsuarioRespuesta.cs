using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUsuarios.Models
{
    public class TipoUsuarioRespuesta
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public List<TipoUsuarioModelo> Resultado { get; set; }
    }
}
