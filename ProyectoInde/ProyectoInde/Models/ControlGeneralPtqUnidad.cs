using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInde.Models
{
    public partial class ControlGeneralPtqUnidad
    {
        public int CodControlGeneralPtqUnidad { get; set; }
        public int PresionAceiteRegulador11261 { get; set; }
        public int PresionAceiteControlPilotoReg11262 { get; set; }
        public int PresionAceiteRefrigeradorReg11263 { get; set; }
        public int TempAceiteReguladorC1180 { get; set; }
        public int TempCojineteGuiaTurbinaC2722 { get; set; }
        public int PAeEntradaEnfriadorCGBar26434 { get; set; }
        public int QAeCGLMinX20E2727 { get; set; }
        public int PAeSalidaEnfriadorCGBar26435 { get; set; }
        public int TAeSalidaEnfriadorCG27362 { get; set; }
        public int TAceiteCGSalidaEnfriadorC27331 { get; set; }
        public int TAceiteCGEntradaEnfriadorC27331 { get; set; }
        public int FlujoAceiteCGTurbinaLMinx5E2727 { get; set; }
        public int FlujoAeCCombinado { get; set; }
        public int FlujoAeGenerador { get; set; }
        public int PAguasAbajoVeBar654 { get; set; }
        public int PAguasArribaVeBar853 { get; set; }
        public int PAguasArribaVeBar651 { get; set; }
        public int PAceiteMandoVeBar652 { get; set; }
        public int IndNivelDesfogueNormalAnormal { get; set; }
        public int PBombaAeBar1894 { get; set; }
        public int PAeGeneradorBar2740 { get; set; }
        public int TAeTurbinaC27361 { get; set; }
        public int PAeTurbinaBar26431 { get; set; }
        public int PAlKgCm226432 { get; set; }
        [DataType(DataType.Date)]
        public DateTime FecIngreso { get; set; }
        public int CodHora2 { get; set; }
        public int CodUnidadGeneradora { get; set; }

        public virtual Hora2 CodHora2Navigation { get; set; }
        public virtual UnidadGeneradora CodUnidadGeneradoraNavigation { get; set; }
    }
}
