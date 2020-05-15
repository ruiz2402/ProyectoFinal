using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoInde.Controllers
{
    public class miFormulario
    {
        public String fechaInicial { get; set; }

        public String fechaFinal { get; set; }

        public String transformador { get; set; }

        public TimeSpan horaInicial { get; set; }

        public TimeSpan horaFinal { get; set; }

    }
}
