using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInde.Models
{
    public partial class DatoTransformador
    {
        public int CodDatoTransformador { get; set; }
        public int PotenciaMw { get; set; }
        public int TemperaturaAcC { get; set; }
        public int TemperaturaDeC { get; set; }
        public int NivelAc { get; set; }
        public int VentIMA { get; set; }

        [DataType(DataType.Date)]
        public DateTime FecIngreso { get; set; }
        public int CodHora { get; set; }
        public int CodTransformador { get; set; }

        public virtual Hora1 CodHoraNavigation { get; set; }
        public virtual Transformador CodTransformadorNavigation { get; set; }
    }
}
