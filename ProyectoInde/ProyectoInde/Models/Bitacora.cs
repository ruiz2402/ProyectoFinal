using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Bitacora
    {
        public int CodBitacora { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Rol { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Formulario { get; set; }
    }
}
