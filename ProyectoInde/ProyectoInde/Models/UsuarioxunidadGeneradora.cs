using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class UsuarioxunidadGeneradora
    {
        public int CodUsuarioXunidadGeneradora { get; set; }
        public int CodUsuario { get; set; }
        public int CodUnidadGeneradora { get; set; }

        public virtual UnidadGeneradora CodUnidadGeneradoraNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
