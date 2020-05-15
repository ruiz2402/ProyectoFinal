using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Usuariosxrol
    {
        public int CodUsuarioXrol { get; set; }
        public int CodUsuario { get; set; }
        public int CodRol { get; set; }

        public virtual Rol CodRolNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
