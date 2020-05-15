using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInde.Models
{
    public partial class ControlGeneracionDiaria
    {
        public int CodControlGeneracionDiaria { get; set; }
        public int _230kVA { get; set; }
        public int CampoV { get; set; }
        public int CampoA { get; set; }
        public int _138kvA { get; set; }
        public int _138kvKv { get; set; }
        public int PAparenteMw { get; set; }
        public int PAparenteMvar { get; set; }
        [DataType(DataType.Date)]
        public DateTime FecIngreso { get; set; }
        public int CodHora { get; set; }
        public int CodUnidadGeneradora { get; set; }

        public virtual Hora1 CodHoraNavigation { get; set; }
        public virtual UnidadGeneradora CodUnidadGeneradoraNavigation { get; set; }
    }
}
