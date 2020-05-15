using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Hora1
    {
        public Hora1()
        {
            ControlGeneracionDiaria = new HashSet<ControlGeneracionDiaria>();
            ControlParametroUnidad = new HashSet<ControlParametroUnidad>();
            DatoTransformador = new HashSet<DatoTransformador>();
        }

        public int CodHora { get; set; }
        public TimeSpan Hora { get; set; }

        public virtual ICollection<ControlGeneracionDiaria> ControlGeneracionDiaria { get; set; }
        public virtual ICollection<ControlParametroUnidad> ControlParametroUnidad { get; set; }
        public virtual ICollection<DatoTransformador> DatoTransformador { get; set; }
    }
}
