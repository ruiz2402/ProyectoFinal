using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuariosxrol = new HashSet<Usuariosxrol>();
        }

        public int CodRol { get; set; }
        public string Rol1 { get; set; }

        public virtual ICollection<Usuariosxrol> Usuariosxrol { get; set; }
    }
}
