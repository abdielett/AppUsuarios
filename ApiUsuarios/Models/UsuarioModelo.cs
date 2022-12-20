using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUsuarios.Models
{
    public class UsuarioModelo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public TipoUsuarioModelo TipoUsuario { get; set; }
    }
}
