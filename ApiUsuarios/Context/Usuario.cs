using System;
using System.Collections.Generic;

#nullable disable

namespace ApiUsuarios.Context
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public int? TipoUsuario { get; set; }

        public virtual TipoUsuario TipoUsuarioNavigation { get; set; }
    }
}
