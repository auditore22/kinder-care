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

    public virtual DbSet<Actividades> Actividades { get; set; }

    public virtual DbSet<Alergias> Alergias { get; set; }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<CondicionesMedicas> CondicionesMedicas { get; set; }

    public virtual DbSet<ContactosEmergencia> ContactosEmergencia { get; set; }

    public virtual DbSet<Docentes> Docentes { get; set; }

    public virtual DbSet<Evaluaciones> Evaluaciones { get; set; }

    public virtual DbSet<Medicamentos> Medicamentos { get; set; }

    public virtual DbSet<Ninos> Ninos { get; set; }

    public virtual DbSet<ObservacionesDocentes> ObservacionesDocentes { get; set; }

    public virtual DbSet<Pagos> Pagos { get; set; }

    public virtual DbSet<ProgresoAcademico> ProgresoAcademico { get; set; }

    public virtual DbSet<RelDocenteNinoMateria> RelDocenteNinoMateria { get; set; }

    public virtual DbSet<RelNinoActividad> RelNinoActividad { get; set; }
    public virtual DbSet<RelPadresNinos> RelPadresNinos { get; set; }

    public virtual DbSet<RelNinoAlergia> RelNinoAlergia { get; set; }

    public virtual DbSet<RelNinoMedicamento> RelNinoMedicamento { get; set; }

    public virtual DbSet<RelNinoCondicion> RelNinoCondicion { get; set; }

    public virtual DbSet<RelNinoContactoEmergencia> RelNinoContactoEmergencia { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<TipoActividad> TipoActividad { get; set; }

    public virtual DbSet<TipoPagos> TipoPagos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public DbSet<ExpedienteCompletoNino> VwExpedienteCompletoNino { get; set; }

    public virtual DbSet<Tareas> Tareas { get; set; }

    public virtual DbSet<RelNinoTarea> RelNinoTarea { get; set; }

    public virtual DbSet<Niveles> Niveles { get; set; }

    public DbSet<Documentos> Documentos { get; set; }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=kinder_care;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividades>(entity =>
        {
            entity.HasKey(e => e.IdActividad).HasName("PK__activida__071C9F95FDAD0639");

            entity.ToTable("actividades");

            entity.Property(e => e.IdActividad).HasColumnName("id_Actividad");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdTipoActividad).HasColumnName("id_tipo_actividad");
            entity.Property(e => e.Lugar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lugar");

            // Nueva propiedad Descripcion
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");

            entity.HasOne(d => d.IdTipoActividadNavigation).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.IdTipoActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__actividad__id_ti__02FC7413");
        });

        modelBuilder.Entity<Alergias>(entity =>
        {
            entity.HasKey(e => e.IdAlergia).HasName("PK__alergias__148C09CA3541F7AD");

            entity.ToTable("alergias");

            entity.Property(e => e.IdAlergia).HasColumnName("id_Alergia");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreAlergia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_alergia");
        });

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__asistenc__AFB810EF003C5B5E");

            entity.ToTable("asistencia");

            entity.HasIndex(e => e.IdNino, "idx_asistencia_nino");

            entity.Property(e => e.IdAsistencia).HasColumnName("id_Asistencia");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Presente).HasColumnName("presente").IsRequired();
            entity.Property(e => e.IdNino).HasColumnName("id_nino");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asistencia_nino");
        });

        modelBuilder.Entity<CondicionesMedicas>(entity =>
        {
            entity.HasKey(e => e.IdCondicionMedica).HasName("PK__condicio__AF5CC79BA27B8280");

            entity.ToTable("condiciones_medicas");

            entity.Property(e => e.IdCondicionMedica).HasColumnName("id_Condicion_medica");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreCondicion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_condicion");
        });

        modelBuilder.Entity<ContactosEmergencia>(entity =>
        {
            entity.HasKey(e => e.IdContactoEmergencia).HasName("PK__contacto__B407E9F29A28ED1F");

            entity.ToTable("contactos_emergencia");

            entity.Property(e => e.IdContactoEmergencia).HasColumnName("id_Contacto_Emergencia");
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
            entity.Property(e => e.Telefono).HasColumnName("telefono");
        });

        modelBuilder.Entity<Docentes>(entity =>
        {
            entity.HasKey(e => e.IdDocente).HasName("PK__docentes__EBE50C3E0B7E379A");

            entity.ToTable("docentes", tb => tb.HasTrigger("trg_update_audit_docentes"));

            entity.Property(e => e.IdDocente).HasColumnName("id_Docente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
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
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_docentes_usuario");
        });

        modelBuilder.Entity<Evaluaciones>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("PK__evaluaci__6E22DFF602DC711F");

            entity.ToTable("evaluaciones", tb => tb.HasTrigger("trg_update_audit_evaluaciones"));

            entity.HasIndex(e => e.IdNino, "idx_evaluaciones_nino");

            entity.Property(e => e.IdEvaluacion).HasColumnName("id_Evaluacion");
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

        modelBuilder.Entity<Medicamentos>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__medicame__DA5E04EB0503C2D4");

            entity.ToTable("medicamentos");

            entity.Property(e => e.IdMedicamento).HasColumnName("id_Medicamento");
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

        /*modelBuilder.Entity<Ninos>(entity =>
        {
            entity.HasKey(e => e.IdNino).HasName("PK__ninos__3CAF0674B917C51C");

            entity.ToTable("ninos", tb => tb.HasTrigger("trg_update_audit_ninos"));

            entity.HasIndex(e => e.NombreNino, "idx_ninos_nombre");

            entity.Property(e => e.IdNino).HasColumnName("id_Nino");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula");
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
        });*/

        modelBuilder.Entity<Ninos>(entity =>
        {
            entity.HasKey(e => e.IdNino).HasName("PK__ninos__3CAF067422D54C89");

            entity.ToTable("ninos", tb => tb.HasTrigger("trg_update_audit_ninos"));

            entity.HasIndex(e => e.NombreNino, "idx_ninos_nombre");

            entity.Property(e => e.IdNino).HasColumnName("id_Nino");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Direccion)
                .HasColumnType("text") // Original usaba "text"
                .HasMaxLength(256) // Se agrega la longitud del segundo contexto
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdNivel)
                .HasDefaultValue(1)
                .HasColumnName("id_nivel"); // Agregado desde el segundo contexto
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

            // Relación con niveles
            entity.HasOne(d => d.IdNivelNavigation).WithMany(p => p.Ninos)
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ninos_Niveles"); // Agregado desde el segundo contexto

            // Configuración de relación con RelNinoTarea (original)
            entity.HasMany(n => n.RelNinoTarea)
                .WithOne(rt => rt.Ninos)
                .HasForeignKey(rt => rt.IdNino);
        });

        modelBuilder.Entity<Niveles>(entity =>
        {
            entity.HasKey(e => e.IdNivel).HasName("PK__niveles__9CAF1C53B9DA917D");

            entity.ToTable("niveles");

            entity.HasIndex(e => e.Nombre, "UQ__niveles__72AFBCC67BF9D41F").IsUnique();

            entity.Property(e => e.IdNivel).HasColumnName("id_nivel");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        // Configuración para Tareas
        modelBuilder.Entity<Tareas>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PK__tareas__C0ECF707A3D6C37C");

            entity.ToTable("tareas");

            entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
            entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion)
                .HasDefaultValue(0)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaAsignada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha_asignada");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("date")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");

            // Relación con RelNinoTarea
            entity.HasMany(t => t.RelNinoTarea)
                .WithOne(rt => rt.Tareas)
                .HasForeignKey(rt => rt.IdTarea);

            // Relación con el documento del docente
            entity.HasOne(d => d.DocTareaDocente)
                .WithMany()
                .HasForeignKey(d => d.IdDocDocente);
        });

        modelBuilder.Entity<Documentos>(entity =>
        {
            entity.HasKey(d => d.IdDoc); // Definimos la clave primaria explícitamente
        });

        modelBuilder.Entity<RelNinoTarea>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdTarea }).HasName("PK__rel_nino_tarea");

            entity.ToTable("rel_nino_tarea");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
            entity.Property(e => e.Calificacion)
                .HasDefaultValue(0)
                .HasColumnName("calificacion");
            entity.Property(e => e.IdDocNino).HasColumnName("id_doc_nino");

            // Relación con Tareas
            entity.HasOne(rt => rt.Tareas)
                .WithMany(t => t.RelNinoTarea)
                .HasForeignKey(rt => rt.IdTarea);

            // Configuración de relación con Documentos (sin necesidad de redefinir la propiedad de navegación)
            entity.HasOne(rt => rt.DocTareaNino)
                .WithMany()
                .HasForeignKey(rt => rt.IdDocNino);
        });

        modelBuilder.Entity<ObservacionesDocentes>(entity =>
        {
            entity.HasKey(e => e.IdObservacionDocente).HasName("PK__observac__FCB6ACAC58572707");

            entity.ToTable("observaciones_docentes");

            entity.Property(e => e.IdObservacionDocente).HasColumnName("id_Observacion_Docente");
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

        modelBuilder.Entity<Pagos>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__pagos__2A3B86B22059D92F");

            entity.ToTable("pagos", tb => tb.HasTrigger("trg_update_audit_pagos"));

            entity.HasIndex(e => e.IdPadre, "idx_pagos_padre");

            entity.Property(e => e.IdPago).HasColumnName("id_Pago");
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
        });

        modelBuilder.Entity<ProgresoAcademico>(entity =>
        {
            entity.HasKey(e => e.IdProgresoAcademico).HasName("PK__progreso__20F2A38CE3214FF3");

            entity.ToTable("progreso_academico", tb => tb.HasTrigger("trg_update_audit_progreso_academico"));

            entity.Property(e => e.IdProgresoAcademico).HasColumnName("id_Progreso_Academico");
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

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.ProgresoAcademico)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_progreso_nino");
        });

        modelBuilder.Entity<RelDocenteNinoMateria>(entity =>
        {
            entity.HasKey(e => new { e.IdDocente, e.IdNino }).HasName("PK__rel_doce__42BD208A0B7446B8");

            entity.ToTable("rel_docente_nino_materia");

            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_do__5FB337D6");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_ni__60A75C0F");
        });


        // Relación Niño-Alergia
        modelBuilder.Entity<RelNinoAlergia>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdAlergia });

            entity.ToTable(
                "rel_nino_alergia"); // Asegúrate de que este nombre sea el correcto de la tabla en la base de datos
            entity.Property(e => e.IdNino).HasColumnName("id_nino"); // Nombre de la columna correcta
            entity.Property(e => e.IdAlergia).HasColumnName("id_alergia"); // Nombre de la columna correcta

            entity.HasOne(d => d.Nino)
                .WithMany(p => p.RelNinoAlergia)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Alergia)
                .WithMany(p => p.RelNinoAlergia)
                .HasForeignKey(d => d.IdAlergia)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Relación Niño-Medicamento
        modelBuilder.Entity<RelNinoMedicamento>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdMedicamento });

            entity.ToTable("rel_nino_medicamento"); // Asegúrate de usar el nombre correcto
            entity.Property(e => e.IdNino).HasColumnName("id_nino"); // Nombre de la columna correcta
            entity.Property(e => e.IdMedicamento).HasColumnName("id_medicamento"); // Nombre de la columna correcta

            entity.HasOne(d => d.Nino)
                .WithMany(p => p.RelNinoMedicamento)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Medicamento)
                .WithMany(p => p.RelNinoMedicamento)
                .HasForeignKey(d => d.IdMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Relación Niño-Condición Médica
        modelBuilder.Entity<RelNinoCondicion>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdCondicion });

            entity.ToTable("rel_nino_condicion"); // Asegúrate de usar el nombre correcto
            entity.Property(e => e.IdNino).HasColumnName("id_nino"); // Nombre de la columna correcta
            entity.Property(e => e.IdCondicion).HasColumnName("id_condicion"); // Nombre de la columna correcta

            entity.HasOne(d => d.Nino)
                .WithMany(p => p.RelNinoCondicion)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Condicion)
                .WithMany(p => p.RelNinoCondicion)
                .HasForeignKey(d => d.IdCondicion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Relación Niño-Contacto de Emergencia
        modelBuilder.Entity<RelNinoContactoEmergencia>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdContacto });

            entity.HasOne(d => d.Nino)
                .WithMany(p => p.RelNinoContactoEmergencia)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ContactoEmergencia)
                .WithMany(p => p.RelNinoContactoEmergencia)
                .HasForeignKey(d => d.IdContacto)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.ToTable("rel_nino_contacto_emergencia");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
        });

        modelBuilder.Entity<RelDocenteNinoMateria>(entity =>
        {
            entity.HasKey(e => new { e.IdDocente, e.IdNino }).HasName("PK__rel_doce__42BD208A0B7446B8");

            entity.ToTable("rel_docente_nino_materia");

            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_do__5FB337D6");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelDocenteNinoMateria)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_docen__id_ni__60A75C0F");
        });

        modelBuilder.Entity<RelNinoActividad>(entity =>
        {
            entity.HasKey(e => new { e.IdNino, e.IdActividad }).HasName("PK__rel_nino__06C41D3E23CF66C7");

            entity.ToTable("rel_nino_actividad");

            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.IdActividad).HasColumnName("id_actividad");
            entity.Property(e => e.Asistencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("asistencia");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.RelNinoActividad)
                .HasForeignKey(d => d.IdActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_nino___id_ac__07C12930");

            entity.HasOne(d => d.IdNinoNavigation).WithMany(p => p.RelNinoActividad)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__rel_nino___id_ni__06CD04F7");
        });

        modelBuilder.Entity<RelPadresNinos>(entity =>
        {
            entity.HasKey(e => new { e.IdPadre, e.IdNino }).HasName("PK__rel_padr__42228E4E09B5059C");

            entity.ToTable("rel_padres_ninos");

            entity.HasIndex(e => e.IdNino, "idx_rel_padres_ninos_nino");

            entity.Property(e => e.IdPadre).HasColumnName("id_padre");
            entity.Property(e => e.IdNino).HasColumnName("id_nino");
            entity.Property(e => e.Relacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relacion");

            entity.HasOne(d => d.IdNinoNavigation)
                .WithMany(p => p.RelPadresNinos)
                .HasForeignKey(d => d.IdNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_padres_ninos_nino");

            entity.HasOne(d => d.IdPadreNavigation)
                .WithMany(p => p.RelPadresNinos)
                .HasForeignKey(d => d.IdPadre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_padres_ninos_padre");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__roles__76482FD2122BF699");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "UQ__roles__72AFBCC692ECDCF1").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_Rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoActividad>(entity =>
        {
            entity.HasKey(e => e.IdTipoActividad).HasName("PK__tipo_act__03B324327EFE116E");

            entity.ToTable("tipo_actividad");

            entity.HasIndex(e => e.NombreTipoActividad, "UQ__tipo_act__717F08BCA36A2042").IsUnique();

            entity.Property(e => e.IdTipoActividad).HasColumnName("id_Tipo_Actividad");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.NombreTipoActividad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo_actividad");
        });

        modelBuilder.Entity<TipoPagos>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__tipo_pag__F7E781E5D145505F");

            entity.ToTable("tipo_pagos");

            entity.HasIndex(e => e.NombreTipoPago, "UQ__tipo_pag__3E5147E208217D9B").IsUnique();

            entity.Property(e => e.IdTipoPago).HasColumnName("id_tipo_pago");
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

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__8E901EAA42E54E3E");

            entity.ToTable("usuarios", tb =>
            {
                tb.HasTrigger("trg_insert_docente");
                tb.HasTrigger("trg_update_Rol_Docente");
                tb.HasTrigger("trg_update_audit_usuarios");
            });

            entity.HasIndex(e => e.IdRol, "idx_usuarios_rol");

            entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contrasena_hash");
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
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumTelefono).HasColumnName("num_Telefono");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_rol");
        });

        modelBuilder.Entity<ExpedienteCompletoNino>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_ExpedienteCompletoNino");
            entity.Property(e => e.IdNino).HasColumnName("id_Nino");
            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.NombreNino).HasColumnName("nombre_nino");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Poliza).HasColumnName("poliza");
            entity.Property(e => e.NombreAlergia).HasColumnName("nombre_alergia");
            entity.Property(e => e.NombreCondicion).HasColumnName("nombre_condicion");
            entity.Property(e => e.NombreMedicamento).HasColumnName("nombre_medicamento");
            entity.Property(e => e.Dosis).HasColumnName("dosis");
            entity.Property(e => e.NombreContacto).HasColumnName("nombre_contacto");
            entity.Property(e => e.TelefonoContacto).HasColumnName("telefono_contacto");
            entity.Property(e => e.RelacionContacto).HasColumnName("relacion_contacto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}