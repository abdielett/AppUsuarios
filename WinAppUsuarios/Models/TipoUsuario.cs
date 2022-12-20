using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinAppUsuarios.Models
{
    public class TipoUsuario
    {
       public int ID { get; set; }
       public string Nombre { get; set; }
       public TipoUsuario(int ID, string Nombre)
        {
            this.ID = ID;
            this.Nombre = Nombre;
        }
    }
}
