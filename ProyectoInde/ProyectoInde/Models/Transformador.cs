using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Transformador
    {
        public Transformador()
        {
            DatoTransformador = new HashSet<DatoTransformador>();
            Usuarioxtransformador = new HashSet<Usuarioxtransformador>();
        }

        public int CodTransformador { get; set; }
        public string Transformador1 { get; set; }

        public virtual ICollection<DatoTransformador> DatoTransformador { get; set; }
        public virtual ICollection<Usuarioxtransformador> Usuarioxtransformador { get; set; }
    }
}
