using Microsoft.EntityFrameworkCore;

namespace kinder_care.Models;

public partial class KinderCareContext : DbContext
{
    public KinderCareContext()
    {
    }

    public KinderCareContext(DbContextOptions<KinderCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividade> Actividades { get; set; }

    public virtual DbSet<Alergia> Alergias { get; set; }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<CondicionesMedica> CondicionesMedicas { get; set; }

    public virtual DbSet<ContactosEmergencium> ContactosEmergencia { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Nino> Ninos { get; set; }

    public virtual DbSet<ObservacionesDocente> ObservacionesDocentes { get; set; }

    public virtual DbSet<Padre> Padres { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<ProgresoAcademico> ProgresoAcademicos { get; set; }

    public virtual DbSet<RelDocenteNinoMaterium> RelDocenteNinoMateria { get; set; }

    public virtual DbSet<RelNinoActividad> RelNinoActividads { get; set; }

    public virtual DbSet<RelPadresNino> RelPadresNinos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoActividad> TipoActividads { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwActividadesNino> VwActividadesNinos { get; set; }

    public virtual DbSet<VwAsistenciaPorNino> VwAsistenciaPorNinos { get; set; }

    public virtual DbSet<VwContactosEmergenciaPorNino> VwContactosEmergenciaPorNinos { get; set; }

    public virtual DbSet<VwEvaluacionesPorNino> VwEvaluacionesPorNinos { get; set; }

    public virtual DbSet<VwExpediente> VwExpedientes { get; set; }

    public virtual DbSet<VwNinosAlergia> VwNinosAlergias { get; set; }

    public virtual DbSet<VwNinosCondicionesMedica> VwNinosCondicionesMedicas { get; set; }

    public virtual DbSet<VwNinosMedicamento> VwNinosMedicamentos { get; set; }

    public virtual DbSet<VwNinosPorDocente> VwNinosPorDocentes { get; set; }

    public virtual DbSet<VwNinosPorNombre> VwNinosPorNombres { get; set; }

    public virtual DbSet<VwPadresConUsuario> VwPadresConUsuarios { get; set; }

    public virtual DbSet<VwPagosPorPadre> VwPagosPorPadres { get; set; }

    public virtual DbSet<VwProfesoresPorNino> VwProfesoresPorNinos { get; set; }

    public virtual DbSet<VwRelPadresNino> VwRelPadresNinos { get; set; }

    public virtual DbSet<VwReporteProgresoAcademico> VwReporteProgresoAcademicos { get; set; }

    public virtual DbSet<VwUsuariosPorRol> VwUsuariosPorRols { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=kinder_care;Trusted_Connection=True;")

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__activida__3213E83FA1253B31");

            entity.ToTable("actividades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdTipoActividad).HasColumnName("id_tipo_actividad");
            entity.Property(e => e.Lugar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lugar");

            entity.HasOne(d => d.IdTipoActividadNavigation).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.IdTipoActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__actividad__id_ti__08B54D69");
        });

        modelBuilder.Entity<Alergia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__alergias__3213E83FDEDC4112");

            entity.ToTable("alergias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreAlergia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_alergia");
        });

        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asistenc__3213E83F3DA04B78");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.IdNino, "idx_asistencia_nino");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada).HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asistencia_nino");
        });

        modelBuilder.Entity<CondicionesMedica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__condicio__3213E83FEAC3F4A1");

            entity.ToTable("condiciones_medicas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreCondicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_condicion");
        });

        modelBuilder.Entity<ContactosEmergencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contacto__3213E83F4D88C9DB");

            entity.ToTable("contactos_emergencia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.Relacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relacion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docentes__3213E83F432D2089");

            entity.ToTable("docentes", tb => tb.HasTrigger("trg_update_audit_docentes"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.GrupoAsignado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("grupo_asignado");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docentes_usuario");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__evaluaci__3213E83FE021A9B9");

            entity.ToTable("evaluaciones", tb => tb.HasTrigger("trg_update_audit_evaluaciones"));

            entity.HasIndex(e => e.IdNino, "idx_evaluaciones_nino");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Asignatura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Puntaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("puntaje");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evaluaciones_nino");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medicame__3213E83F69DEA303");

            entity.ToTable("medicamentos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Dosis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dosis");
            entity.Property(e => e.NombreMedicamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_medicamento");
        });

        modelBuilder.Entity<Nino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ninos__3213E83F904C2E6C");

            entity.ToTable("ninos", tb => tb.HasTrigger("trg_update_audit_ninos"));

            entity.HasIndex(e => e.NombreNino, "idx_ninos_nombre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.Poliza)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("poliza");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasMany(d => d.IdAlergia).WithMany(p => p.IdNinos)
                .UsingEntity<Dictionary<string, object>>(
                    "RelNinoAlergium",
                    r => r.HasOne<Alergia>().WithMany()
                        .HasForeignKey("IdAlergia")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_al__797309D9"),
                    l => l.HasOne<Nino>().WithMany()
                        .HasForeignKey("IdNino")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_ni__787EE5A0"),
                    j =>
                    {
                        j.HasKey("IdNino", "IdAlergia").HasName("PK__rel_nino__28DC7B4894297837");
                        j.ToTable("rel_nino_alergia");
                        j.IndexerProperty<int>("IdNino").HasColumnName("id_nino");
                        j.IndexerProperty<int>("IdAlergia").HasColumnName("id_alergia");
                    });

            entity.HasMany(d => d.IdCondicions).WithMany(p => p.IdNinos)
                .UsingEntity<Dictionary<string, object>>(
                    "RelNinoCondicion",
                    r => r.HasOne<CondicionesMedica>().WithMany()
                        .HasForeignKey("IdCondicion")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_co__7D439ABD"),
                    l => l.HasOne<Nino>().WithMany()
                        .HasForeignKey("IdNino")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_ni__7C4F7684"),
                    j =>
                    {
                        j.HasKey("IdNino", "IdCondicion").HasName("PK__rel_nino__379B1EF6A9EB6C70");
                        j.ToTable("rel_nino_condicion");
                        j.IndexerProperty<int>("IdNino").HasColumnName("id_nino");
                        j.IndexerProperty<int>("IdCondicion").HasColumnName("id_condicion");
                    });

            entity.HasMany(d => d.IdContactos).WithMany(p => p.IdNinos)
                .UsingEntity<Dictionary<string, object>>(
                    "RelNinoContactoEmergencium",
                    r => r.HasOne<ContactosEmergencium>().WithMany()
                        .HasForeignKey("IdContacto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_co__6D0D32F4"),
                    l => l.HasOne<Nino>().WithMany()
                        .HasForeignKey("IdNino")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_ni__6C190EBB"),
                    j =>
                    {
                        j.HasKey("IdNino", "IdContacto").HasName("PK__rel_nino__BB908C9D0565C8A0");
                        j.ToTable("rel_nino_contacto_emergencia");
                        j.IndexerProperty<int>("IdNino").HasColumnName("id_nino");
                        j.IndexerProperty<int>("IdContacto").HasColumnName("id_contacto");
                    });

            entity.HasMany(d => d.IdMedicamentos).WithMany(p => p.IdNinos)
                .UsingEntity<Dictionary<string, object>>(
                    "RelNinoMedicamento",
                    r => r.HasOne<Medicamento>().WithMany()
                        .HasForeignKey("IdMedicamento")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_me__01142BA1"),
                    l => l.HasOne<Nino>().WithMany()
                        .HasForeignKey("IdNino")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__rel_nino___id_ni__00200768"),
                    j =>
                    {
                        j.HasKey("IdNino", "IdMedicamento").HasName("PK__rel_nino__1951A5B5529878A8");
                        j.ToTable("rel_nino_medicamento");
                        j.IndexerProperty<int>("IdNino").HasColumnName("id_nino");
                        j.IndexerProperty<int>("IdMedicamento").HasColumnName("id_medicamento");
                    });
        });

        modelBuilder.Entity<ObservacionesDocente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__observac__3213E83FD3025032");

            entity.ToTable("observaciones_docentes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.ObservacionesDocentes)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_observacion_docente");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.ObservacionesDocentes)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_observacion_nino");
        });

        modelBuilder.Entity<Padre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__padres__3213E83F1493F4AA");

            entity.ToTable("padres", tb => tb.HasTrigger("trg_update_audit_padres"));

            entity.HasIndex(e => e.IdUsuario, "idx_padres_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Padres)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_padres_usuario");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pagos__3213E83F0BC82A12");

            entity.ToTable("pagos", tb => tb.HasTrigger("trg_update_audit_pagos"));

            entity.HasIndex(e => e.IdPadre, "idx_pagos_padre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdPadre).HasColumnName("id_padre");
            entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.ReferenciaFactura)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("referencia_factura");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_nino");

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdPadre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_padre");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdTipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_tipo");
        });

        modelBuilder.Entity<ProgresoAcademico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__progreso__3213E83F00D1B772");

            entity.ToTable("progreso_academico", tb => tb.HasTrigger("trg_update_audit_progreso_academico"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaAcademica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("area_academica");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NivelProgreso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nivel_progreso");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.ProgresoAcademicos)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_progreso_nino");
        });

        modelBuilder.Entity<RelDocenteNinoMaterium>(entity =>
        {
            entity.HasKey(e => new { e.IdDocente, e.IdNino }).HasName("PK__rel_doce__42BD208A216C6602");

            entity.ToTable("rel_docente_nino_materia");

            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Materia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("materia");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_do__6477ECF3");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_ni__656C112C");
        });

        modelBuilder.Entity<RelNinoActividad>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdActividad }).HasName("PK__rel_nino__06C41D3ECF48F281");

            entity.ToTable("rel_nino_actividad");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdActividad).HasColumnName("id_actividad");
            entity.Property(e => e.Asistencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asistencia");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.RelNinoActividads)
                .HasForeignKey(d => d.IdActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_nino___id_ac__0D7A0286");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelNinoActividads)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_nino___id_ni__0C85DE4D");
        });

        modelBuilder.Entity<RelPadresNino>(entity =>
        {
            entity.HasKey(e => new { e.IdPadre, e.IdNino }).HasName("PK__rel_padr__42228E4E54BA9444");

            entity.ToTable("rel_padres_ninos");

            entity.HasIndex(e => e.IdNino, "idx_rel_padres_ninos_nino");

            entity.Property(e => e.IdPadre).HasColumnName("id_padre");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Relacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relacion");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelPadresNinos)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_padres_ninos_nino");

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.RelPadresNinos)
                .HasForeignKey(d => d.IdPadre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_padres_ninos_padre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F8EFCB2A1");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "UQ__roles__72AFBCC672463157").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoActividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_act__3213E83F1F3F0A71");

            entity.ToTable("tipo_actividad");

            entity.HasIndex(e => e.NombreTipoActividad, "UQ__tipo_act__717F08BCA6E40BD1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreTipoActividad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_actividad");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipo_pag__3213E83FF90AC59C");

            entity.ToTable("tipo_pagos");

            entity.HasIndex(e => e.NombreTipoPago, "UQ__tipo_pag__3E5147E2CADE5792").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreTipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_pago");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F4C54EA47");

            entity.ToTable("usuarios", tb => tb.HasTrigger("trg_update_audit_usuarios"));

            entity.HasIndex(e => e.IdRol, "idx_usuarios_rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_rol");
        });

        modelBuilder.Entity<VwActividadesNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_actividades_nino");

            entity.Property(e => e.Asistencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asistencia");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreTipoActividad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_actividad");
        });

        modelBuilder.Entity<VwAsistenciaPorNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_asistencia_por_nino");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada).HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwContactosEmergenciaPorNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_contactos_emergencia_por_nino");

            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.Relacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relacion");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VwEvaluacionesPorNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_evaluaciones_por_nino");

            entity.Property(e => e.Asignatura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.Puntaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("puntaje");
        });

        modelBuilder.Entity<VwExpediente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_expediente");

            entity.Property(e => e.AreaAcademica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("area_academica");
            entity.Property(e => e.Asignatura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Asistencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asistencia");
            entity.Property(e => e.ComentariosEvaluacion)
                .HasColumnType("text")
                .HasColumnName("comentarios_evaluacion");
            entity.Property(e => e.DescripcionObservacion)
                .HasColumnType("text")
                .HasColumnName("descripcion_observacion");
            entity.Property(e => e.DescripcionProgreso)
                .HasColumnType("text")
                .HasColumnName("descripcion_progreso");
            entity.Property(e => e.FechaActividad).HasColumnName("fecha_actividad");
            entity.Property(e => e.FechaEvaluacion).HasColumnName("fecha_evaluacion");
            entity.Property(e => e.FechaObservacion).HasColumnName("fecha_observacion");
            entity.Property(e => e.FechaProgreso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_progreso");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NivelProgreso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nivel_progreso");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.NombreTipoActividad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_actividad");
            entity.Property(e => e.Puntaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("puntaje");
        });

        modelBuilder.Entity<VwNinosAlergia>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ninos_alergias");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreAlergia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_alergia");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwNinosCondicionesMedica>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ninos_condiciones_medicas");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreCondicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_condicion");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwNinosMedicamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ninos_medicamentos");

            entity.Property(e => e.Dosis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dosis");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NombreMedicamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_medicamento");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwNinosPorDocente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ninos_por_docente");

            entity.Property(e => e.ApellidoDocente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido_docente");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Materia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("materia");
            entity.Property(e => e.NombreDocente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_docente");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwNinosPorNombre>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ninos_por_nombre");

            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.Poliza)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("poliza");
        });

        modelBuilder.Entity<VwPadresConUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_padres_con_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
        });

        modelBuilder.Entity<VwPagosPorPadre>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_pagos_por_padre");

            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdPadre).HasColumnName("id_padre");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.NombrePadre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_padre");
            entity.Property(e => e.NombreTipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_pago");
            entity.Property(e => e.ReferenciaFactura)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("referencia_factura");
        });

        modelBuilder.Entity<VwProfesoresPorNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_profesores_por_nino");

            entity.Property(e => e.ApellidoDocente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido_docente");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Materia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("materia");
            entity.Property(e => e.NombreDocente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_docente");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
        });

        modelBuilder.Entity<VwRelPadresNino>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_rel_padres_ninos");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdPadre).HasColumnName("id_padre");
            entity.Property(e => e.IdUsuarioPadre).HasColumnName("id_usuario_padre");
            entity.Property(e => e.NombreNino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_nino");
            entity.Property(e => e.NombrePadre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_padre");
            entity.Property(e => e.Relacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relacion");
        });

        modelBuilder.Entity<VwReporteProgresoAcademico>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_reporte_progreso_academico");

            entity.Property(e => e.AreaAcademica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("area_academica");
            entity.Property(e => e.Asignatura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.NivelProgreso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nivel_progreso");
            entity.Property(e => e.ObservacionDocente).HasColumnType("text");
            entity.Property(e => e.Puntaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("puntaje");
        });

        modelBuilder.Entity<VwUsuariosPorRol>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_usuarios_por_rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
