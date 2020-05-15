using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoInde.Models
{
    public partial class bd_inde2Context : DbContext
    {
        public bd_inde2Context()
        {
        }

        public bd_inde2Context(DbContextOptions<bd_inde2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<ControlGeneracionDiaria> ControlGeneracionDiaria { get; set; }
        public virtual DbSet<ControlGeneralPtqUnidad> ControlGeneralPtqUnidad { get; set; }
        public virtual DbSet<ControlParametroUnidad> ControlParametroUnidad { get; set; }
        public virtual DbSet<DatoTransformador> DatoTransformador { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Hora1> Hora1 { get; set; }
        public virtual DbSet<Hora2> Hora2 { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Transformador> Transformador { get; set; }
        public virtual DbSet<UnidadGeneradora> UnidadGeneradora { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Usuariosxrol> Usuariosxrol { get; set; }
        public virtual DbSet<Usuarioxtransformador> Usuarioxtransformador { get; set; }
        public virtual DbSet<UsuarioxunidadGeneradora> UsuarioxunidadGeneradora { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=127.0.0.1;database=bd_inde2;user=root", x => x.ServerVersion("10.4.11-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.CodBitacora)
                    .HasName("PRIMARY");

                entity.ToTable("bitacora");

                entity.Property(e => e.CodBitacora)
                    .HasColumnName("cod_bitacora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("fecha_ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.Formulario)
                    .IsRequired()
                    .HasColumnName("formulario")
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnName("rol")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<ControlGeneracionDiaria>(entity =>
            {
                entity.HasKey(e => e.CodControlGeneracionDiaria)
                    .HasName("PRIMARY");

                entity.ToTable("control_generacion_diaria");

                entity.HasIndex(e => e.CodHora)
                    .HasName("fk_control_generacion_diaria_hora1");

                entity.HasIndex(e => e.CodUnidadGeneradora)
                    .HasName("fk_control_generacion_diaria_unidad_generadora");

                entity.Property(e => e.CodControlGeneracionDiaria)
                    .HasColumnName("cod_control_generacion_diaria")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CampoA)
                    .HasColumnName("campo_A")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CampoV)
                    .HasColumnName("campo_V")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodHora)
                    .HasColumnName("cod_hora")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodUnidadGeneradora)
                    .HasColumnName("cod_unidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FecIngreso)
                    .HasColumnName("fec_ingreso")
                    .HasColumnType("date");

                entity.Property(e => e.PAparenteMvar)
                    .HasColumnName("P.Aparente_MVAR")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAparenteMw)
                    .HasColumnName("P.Aparente_MW")
                    .HasColumnType("int(12)");

                entity.Property(e => e._138kvA)
                    .HasColumnName("13.8Kv_A")
                    .HasColumnType("int(12)");

                entity.Property(e => e._138kvKv)
                    .HasColumnName("13.8Kv_kv")
                    .HasColumnType("int(12)");

                entity.Property(e => e._230kVA)
                    .HasColumnName("230kV_A")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodHoraNavigation)
                    .WithMany(p => p.ControlGeneracionDiaria)
                    .HasForeignKey(d => d.CodHora)
                    .HasConstraintName("fk_control_generacion_diaria_hora1");

                entity.HasOne(d => d.CodUnidadGeneradoraNavigation)
                    .WithMany(p => p.ControlGeneracionDiaria)
                    .HasForeignKey(d => d.CodUnidadGeneradora)
                    .HasConstraintName("fk_control_generacion_diaria_unidad_generadora");
            });

            modelBuilder.Entity<ControlGeneralPtqUnidad>(entity =>
            {
                entity.HasKey(e => e.CodControlGeneralPtqUnidad)
                    .HasName("PRIMARY");

                entity.ToTable("control_general_ptq_unidad");

                entity.HasIndex(e => e.CodHora2)
                    .HasName("fk_control_general_ptq_unidad_hora2");

                entity.HasIndex(e => e.CodUnidadGeneradora)
                    .HasName("fk_control_general_ptq_unidad_uidad_generadora");

                entity.Property(e => e.CodControlGeneralPtqUnidad)
                    .HasColumnName("cod_control_general_PTQ_unidad")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodHora2)
                    .HasColumnName("cod_hora2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodUnidadGeneradora)
                    .HasColumnName("cod_unidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FecIngreso)
                    .HasColumnName("fec_ingreso")
                    .HasColumnType("date");

                entity.Property(e => e.FlujoAceiteCGTurbinaLMinx5E2727)
                    .HasColumnName("flujo_aceite_C.G.turbina_L/minx5(e2727)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FlujoAeCCombinado)
                    .HasColumnName("flujo_AE_C.combinado")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FlujoAeGenerador)
                    .HasColumnName("flujo_AE_generador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.IndNivelDesfogueNormalAnormal)
                    .HasColumnName("Ind.Nivel_desfogue(normal/anormal)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAceiteMandoVeBar652)
                    .HasColumnName("P.aceite_mando_VE_bar(65-2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAeEntradaEnfriadorCGBar26434)
                    .HasColumnName("P_AE_entrada_enfriador_C.G.bar(2643/4)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAeGeneradorBar2740)
                    .HasColumnName("P.AE_generador_bar(2740)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAeSalidaEnfriadorCGBar26435)
                    .HasColumnName("P.AE_salida_enfriador_C.G.bar(2643/5)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAeTurbinaBar26431)
                    .HasColumnName("P.AE_turbina_bar(2643/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAguasAbajoVeBar654)
                    .HasColumnName("P.aguas_abajo_VE_bar(65-4)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAguasArribaVeBar651)
                    .HasColumnName("P.aguas_arriba_VE_bar(65-1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAguasArribaVeBar853)
                    .HasColumnName("P.aguas_arriba_VE_bar(85-3)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PAlKgCm226432)
                    .HasColumnName("P.Al_kg/cm2(2643/2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PBombaAeBar1894)
                    .HasColumnName("P.bomba_AE_bar(1894)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAceiteControlPilotoReg11262)
                    .HasColumnName("presion_aceite_control_piloto_reg(1126/2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAceiteRefrigeradorReg11263)
                    .HasColumnName("presion_aceite_refrigerador_reg(1126/3)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAceiteRegulador11261)
                    .HasColumnName("presion_aceite_regulador(1126/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.QAeCGLMinX20E2727)
                    .HasColumnName("Q.AE_C.G.L/min.x20(e2727)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TAceiteCGEntradaEnfriadorC27331)
                    .HasColumnName("T.aceite_C.G.entrada_enfriador°C(2733/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TAceiteCGSalidaEnfriadorC27331)
                    .HasColumnName("T.aceite_C.G.salida_enfriador°C(2733/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TAeSalidaEnfriadorCG27362)
                    .HasColumnName("T.AE_salida_enfriador_C.G(2736/2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TAeTurbinaC27361)
                    .HasColumnName("T.AE_turbina°C(2736/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TempAceiteReguladorC1180)
                    .HasColumnName("temp_aceite_regulador°C(1180)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TempCojineteGuiaTurbinaC2722)
                    .HasColumnName("temp_cojinete_guia_turbina°C(2722)")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodHora2Navigation)
                    .WithMany(p => p.ControlGeneralPtqUnidad)
                    .HasForeignKey(d => d.CodHora2)
                    .HasConstraintName("fk_control_general_ptq_unidad_hora2");

                entity.HasOne(d => d.CodUnidadGeneradoraNavigation)
                    .WithMany(p => p.ControlGeneralPtqUnidad)
                    .HasForeignKey(d => d.CodUnidadGeneradora)
                    .HasConstraintName("fk_control_general_ptq_unidad_uidad_generadora");
            });

            modelBuilder.Entity<ControlParametroUnidad>(entity =>
            {
                entity.HasKey(e => e.CodControlParametroUnidad)
                    .HasName("PRIMARY");

                entity.ToTable("control_parametro_unidad");

                entity.HasIndex(e => e.CodHora)
                    .HasName("fk_control_parametro_unidad_hora1");

                entity.HasIndex(e => e.CodUnidadGeneradora)
                    .HasName("fk_control_parametro_unidad_unidad_generadora");

                entity.Property(e => e.CodControlParametroUnidad)
                    .HasColumnName("cod_control_parametro_unidad")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AceiteCojineteGeneradorC)
                    .HasColumnName("aceite_cojinete_generador°C")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AguaSalienteEnfriadorCojinetesGenC)
                    .HasColumnName("agua_saliente_enfriador_cojinetes_Gen°C")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireLlegadaEnfriadorCNo1)
                    .HasColumnName("aire_llegada_enfriador°C-No.1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireLlegadaEnfriadorCNo2)
                    .HasColumnName("aire_llegada_enfriador°C-No.2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalidaEnfriadorCNo1)
                    .HasColumnName("aire_salida_enfriador°C-No.1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalidaEnfriadorCNo2)
                    .HasColumnName("aire_salida_enfriador°C-No.2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato1)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato2)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato3)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato3")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato4)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato4")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato5)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato5")
                    .HasColumnType("int(12)");

                entity.Property(e => e.AireSalienteEnfriadoresGeneradorCDato6)
                    .HasColumnName("aire_saliente_enfriadores_generador°C_dato6")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CaudalAguaSelloLMx20E2639)
                    .HasColumnName("caudal_agua_sello_l/mx20(e2639)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodHora)
                    .HasColumnName("cod_hora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodUnidadGeneradora)
                    .HasColumnName("cod_unidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteEmpujeC)
                    .HasColumnName("cojinete_empuje°C")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteEmpujeGeneradorCDato1)
                    .HasColumnName("cojinete_empuje_generador°C_dato1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteEmpujeGeneradorCDato2)
                    .HasColumnName("cojinete_empuje_generador°C_dato2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteGuiaGeneradorCDato1)
                    .HasColumnName("cojinete_guia_generador°C_dato1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteGuiaGeneradorCDato2)
                    .HasColumnName("cojinete_guia_generador°C_dato2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteGuiaSuperiorC)
                    .HasColumnName("cojinete_guia_superior°C")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CojineteGuiaTurbinaC)
                    .HasColumnName("cojinete_guia_turbina°C")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato1)
                    .HasColumnName("devanados_estator°C_dato1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato2)
                    .HasColumnName("devanados_estator°C_dato2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato3)
                    .HasColumnName("devanados_estator°C_dato3")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato4)
                    .HasColumnName("devanados_estator°C_dato4")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato5)
                    .HasColumnName("devanados_estator°C_dato5")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato6)
                    .HasColumnName("devanados_estator°C_dato6")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato7)
                    .HasColumnName("devanados_estator°C_dato7")
                    .HasColumnType("int(12)");

                entity.Property(e => e.DevanadosEstatorCDato8)
                    .HasColumnName("devanados_estator°C_dato8")
                    .HasColumnType("int(12)");

                entity.Property(e => e.EntradaSalidaAguaEnfriamientoCDato1)
                    .HasColumnName("entrada_salida_agua_enfriamiento°C_dato1")
                    .HasColumnType("int(12)");

                entity.Property(e => e.EntradaSalidaAguaEnfriamientoCDato2)
                    .HasColumnName("entrada_salida_agua_enfriamiento°C_dato2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FecIngreso)
                    .HasColumnName("fec_ingreso")
                    .HasColumnType("date");

                entity.Property(e => e.PotenciaMw)
                    .HasColumnName("potencia_MW")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAguaSelloEje26431)
                    .HasColumnName("presion_agua_sello_eje(2643/1)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAguaSelloEje26432)
                    .HasColumnName("presion_agua_sello_eje(2643/2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PresionAguaSelloKgCm226422)
                    .HasColumnName("presion_agua_sello_kg/cm2(2642/2)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TemperaturaSelloEje2276)
                    .HasColumnName("temperatura_sello_eje(2276)")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodHoraNavigation)
                    .WithMany(p => p.ControlParametroUnidad)
                    .HasForeignKey(d => d.CodHora)
                    .HasConstraintName("fk_control_parametro_unidad_hora1");

                entity.HasOne(d => d.CodUnidadGeneradoraNavigation)
                    .WithMany(p => p.ControlParametroUnidad)
                    .HasForeignKey(d => d.CodUnidadGeneradora)
                    .HasConstraintName("fk_control_parametro_unidad_unidad_generadora");
            });

            modelBuilder.Entity<DatoTransformador>(entity =>
            {
                entity.HasKey(e => e.CodDatoTransformador)
                    .HasName("PRIMARY");

                entity.ToTable("dato_transformador");

                entity.HasIndex(e => e.CodHora)
                    .HasName("fk_dato_transformador_hora1");

                entity.HasIndex(e => e.CodTransformador)
                    .HasName("fk_dato_transformador_transformador");

                entity.Property(e => e.CodDatoTransformador)
                    .HasColumnName("cod_dato_transformador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodHora)
                    .HasColumnName("cod_hora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodTransformador)
                    .HasColumnName("cod_transformador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.FecIngreso)
                    .HasColumnName("fec_ingreso")
                    .HasColumnType("date");

                entity.Property(e => e.NivelAc)
                    .HasColumnName("nivel_AC(%)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.PotenciaMw)
                    .HasColumnName("potencia_MW")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TemperaturaAcC)
                    .HasColumnName("temperatura_AC(°C)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.TemperaturaDeC)
                    .HasColumnName("temperatura_DE(°C)")
                    .HasColumnType("int(12)");

                entity.Property(e => e.VentIMA)
                    .HasColumnName("vent_I/M/A")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodHoraNavigation)
                    .WithMany(p => p.DatoTransformador)
                    .HasForeignKey(d => d.CodHora)
                    .HasConstraintName("fk_dato_transformador_hora1");

                entity.HasOne(d => d.CodTransformadorNavigation)
                    .WithMany(p => p.DatoTransformador)
                    .HasForeignKey(d => d.CodTransformador)
                    .HasConstraintName("fk_dato_transformador_transformador");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.CodGenero)
                    .HasName("PRIMARY");

                entity.ToTable("genero");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Genero1)
                    .IsRequired()
                    .HasColumnName("genero")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Hora1>(entity =>
            {
                entity.HasKey(e => e.CodHora)
                    .HasName("PRIMARY");

                entity.ToTable("hora1");

                entity.Property(e => e.CodHora)
                    .HasColumnName("cod_hora")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");
            });

            modelBuilder.Entity<Hora2>(entity =>
            {
                entity.HasKey(e => e.CodHora2)
                    .HasName("PRIMARY");

                entity.ToTable("hora2");

                entity.Property(e => e.CodHora2)
                    .HasColumnName("cod_hora2")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.CodRol)
                    .HasName("PRIMARY");

                entity.ToTable("rol");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Rol1)
                    .IsRequired()
                    .HasColumnName("rol")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Transformador>(entity =>
            {
                entity.HasKey(e => e.CodTransformador)
                    .HasName("PRIMARY");

                entity.ToTable("transformador");

                entity.Property(e => e.CodTransformador)
                    .HasColumnName("cod_transformador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Transformador1)
                    .IsRequired()
                    .HasColumnName("transformador")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<UnidadGeneradora>(entity =>
            {
                entity.HasKey(e => e.CodUnidadGeneradora)
                    .HasName("PRIMARY");

                entity.ToTable("unidad_generadora");

                entity.Property(e => e.CodUnidadGeneradora)
                    .HasColumnName("cod_unidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.UnidadGeneradora1)
                    .IsRequired()
                    .HasColumnName("unidad_generadora")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CodUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.CodGenero)
                    .HasName("fk_usuario_genero");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("cod_usuario")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CodGenero)
                    .HasColumnName("cod_genero")
                    .HasColumnType("int(12)");

                entity.Property(e => e.Contrasenia)
                    .IsRequired()
                    .HasColumnName("contrasenia")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FecNacimiento)
                    .HasColumnName("fec_nacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.CodGeneroNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.CodGenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuario_genero");
            });

            modelBuilder.Entity<Usuariosxrol>(entity =>
            {
                entity.HasKey(e => e.CodUsuarioXrol)
                    .HasName("PRIMARY");

                entity.ToTable("usuariosxrol");

                entity.HasIndex(e => e.CodRol)
                    .HasName("fk_usuarioXrol_rol");

                entity.HasIndex(e => e.CodUsuario)
                    .HasName("fk_usuarioXrol_usuario");

                entity.Property(e => e.CodUsuarioXrol)
                    .HasColumnName("cod_usuarioXrol")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodRol)
                    .HasColumnName("cod_rol")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("cod_usuario")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodRolNavigation)
                    .WithMany(p => p.Usuariosxrol)
                    .HasForeignKey(d => d.CodRol)
                    .HasConstraintName("fk_usuarioXrol_rol");

                entity.HasOne(d => d.CodUsuarioNavigation)
                    .WithMany(p => p.Usuariosxrol)
                    .HasForeignKey(d => d.CodUsuario)
                    .HasConstraintName("fk_usuarioXrol_usuario");
            });

            modelBuilder.Entity<Usuarioxtransformador>(entity =>
            {
                entity.HasKey(e => e.CodUsuarioxtransformador)
                    .HasName("PRIMARY");

                entity.ToTable("usuarioxtransformador");

                entity.HasIndex(e => e.CodTransformador)
                    .HasName("fk_usuarioxtransformador_transformador");

                entity.HasIndex(e => e.CodUsuario)
                    .HasName("fk_usaurioxtransformador_usuario");

                entity.Property(e => e.CodUsuarioxtransformador)
                    .HasColumnName("cod_usuarioxtransformador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodTransformador)
                    .HasColumnName("cod_transformador")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("cod_usuario")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodTransformadorNavigation)
                    .WithMany(p => p.Usuarioxtransformador)
                    .HasForeignKey(d => d.CodTransformador)
                    .HasConstraintName("fk_usuarioxtransformador_transformador");

                entity.HasOne(d => d.CodUsuarioNavigation)
                    .WithMany(p => p.Usuarioxtransformador)
                    .HasForeignKey(d => d.CodUsuario)
                    .HasConstraintName("fk_usaurioxtransformador_usuario");
            });

            modelBuilder.Entity<UsuarioxunidadGeneradora>(entity =>
            {
                entity.HasKey(e => e.CodUsuarioXunidadGeneradora)
                    .HasName("PRIMARY");

                entity.ToTable("usuarioxunidad_generadora");

                entity.HasIndex(e => e.CodUnidadGeneradora)
                    .HasName("fk_usuarioXunidad_generadora_unidad_generadora");

                entity.HasIndex(e => e.CodUsuario)
                    .HasName("fk_usuarioXunidad_generadora_usuario");

                entity.Property(e => e.CodUsuarioXunidadGeneradora)
                    .HasColumnName("cod_usuarioXunidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodUnidadGeneradora)
                    .HasColumnName("cod_unidad_generadora")
                    .HasColumnType("int(12)");

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("cod_usuario")
                    .HasColumnType("int(12)");

                entity.HasOne(d => d.CodUnidadGeneradoraNavigation)
                    .WithMany(p => p.UsuarioxunidadGeneradora)
                    .HasForeignKey(d => d.CodUnidadGeneradora)
                    .HasConstraintName("fk_usuarioXunidad_generadora_unidad_generadora");

                entity.HasOne(d => d.CodUsuarioNavigation)
                    .WithMany(p => p.UsuarioxunidadGeneradora)
                    .HasForeignKey(d => d.CodUsuario)
                    .HasConstraintName("fk_usuarioXunidad_generadora_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
