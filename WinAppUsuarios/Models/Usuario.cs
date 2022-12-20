using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinAppUsuarios.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string contrasena { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
       public Usuario(int ID, string Nombre, string Email, string Telefono, string Contrasena, TipoUsuario tipoUsuario) {
            this.ID = ID;
            this.Nombre = Nombre;
            this.Email = Email;
            this.Telefono = Telefono;
            this.contrasena = Contrasena;
            this.TipoUsuario = tipoUsuario;
        }
    }
}
