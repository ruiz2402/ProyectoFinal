using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class Hora2
    {
        public Hora2()
        {
            ControlGeneralPtqUnidad = new HashSet<ControlGeneralPtqUnidad>();
        }

        public int CodHora2 { get; set; }
        public TimeSpan Hora { get; set; }

        public virtual ICollection<ControlGeneralPtqUnidad> ControlGeneralPtqUnidad { get; set; }
    }
}
