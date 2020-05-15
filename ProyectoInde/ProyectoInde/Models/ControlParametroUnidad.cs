using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInde.Models
{
    public partial class ControlParametroUnidad
    {
        public int CodControlParametroUnidad { get; set; }
        public int DevanadosEstatorCDato1 { get; set; }
        public int DevanadosEstatorCDato2 { get; set; }
        public int DevanadosEstatorCDato3 { get; set; }
        public int DevanadosEstatorCDato4 { get; set; }
        public int DevanadosEstatorCDato5 { get; set; }
        public int DevanadosEstatorCDato6 { get; set; }
        public int DevanadosEstatorCDato7 { get; set; }
        public int DevanadosEstatorCDato8 { get; set; }
        public int CojineteEmpujeGeneradorCDato1 { get; set; }
        public int CojineteEmpujeGeneradorCDato2 { get; set; }
        public int CojineteGuiaGeneradorCDato1 { get; set; }
        public int CojineteGuiaGeneradorCDato2 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato1 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato2 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato3 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato4 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato5 { get; set; }
        public int AireSalienteEnfriadoresGeneradorCDato6 { get; set; }
        public int EntradaSalidaAguaEnfriamientoCDato1 { get; set; }
        public int EntradaSalidaAguaEnfriamientoCDato2 { get; set; }
        public int AguaSalienteEnfriadorCojinetesGenC { get; set; }
        public int AceiteCojineteGeneradorC { get; set; }
        public int CojineteGuiaTurbinaC { get; set; }
        public int CaudalAguaSelloLMx20E2639 { get; set; }
        public int PresionAguaSelloKgCm226422 { get; set; }
        public int TemperaturaSelloEje2276 { get; set; }
        public int CojineteEmpujeC { get; set; }
        public int AireLlegadaEnfriadorCNo1 { get; set; }
        public int AireLlegadaEnfriadorCNo2 { get; set; }
        public int CojineteGuiaSuperiorC { get; set; }
        public int AireSalidaEnfriadorCNo1 { get; set; }
        public int AireSalidaEnfriadorCNo2 { get; set; }
        public int PresionAguaSelloEje26432 { get; set; }
        public int PresionAguaSelloEje26431 { get; set; }
        public int PotenciaMw { get; set; }
        [DataType(DataType.Date)]
        public DateTime FecIngreso { get; set; }
        public int CodHora { get; set; }
        public int CodUnidadGeneradora { get; set; }

        public virtual Hora1 CodHoraNavigation { get; set; }
        public virtual UnidadGeneradora CodUnidadGeneradoraNavigation { get; set; }
    }
}
