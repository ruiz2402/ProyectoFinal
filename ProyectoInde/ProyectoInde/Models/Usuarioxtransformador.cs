using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Usuarioxtransformador
    {
        public int CodUsuarioxtransformador { get; set; }
        public int CodUsuario { get; set; }
        public int CodTransformador { get; set; }

        public virtual Transformador CodTransformadorNavigation { get; set; }
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
