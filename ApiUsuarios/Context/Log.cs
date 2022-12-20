using System;
using System.Collections.Generic;

#nullable disable

namespace ApiUsuarios.Context
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
