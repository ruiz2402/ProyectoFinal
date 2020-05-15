using System;
using System.Collections.Generic;

namespace ProyectoInde.Models
{
    public partial class UnidadGeneradora
    {
        public UnidadGeneradora()
        {
            ControlGeneracionDiaria = new HashSet<ControlGeneracionDiaria>();
            ControlGeneralPtqUnidad = new HashSet<ControlGeneralPtqUnidad>();
            ControlParametroUnidad = new HashSet<ControlParametroUnidad>();
            UsuarioxunidadGeneradora = new HashSet<UsuarioxunidadGeneradora>();
        }

        public int CodUnidadGeneradora { get; set; }
        public string UnidadGeneradora1 { get; set; }

        public virtual ICollection<ControlGeneracionDiaria> ControlGeneracionDiaria { get; set; }
        public virtual ICollection<ControlGeneralPtqUnidad> ControlGeneralPtqUnidad { get; set; }
        public virtual ICollection<ControlParametroUnidad> ControlParametroUnidad { get; set; }
        public virtual ICollection<UsuarioxunidadGeneradora> UsuarioxunidadGeneradora { get; set; }
    }
}
