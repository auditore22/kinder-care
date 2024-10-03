-------------------------------------------- SPs --------------------------------------------
-- SP para la creación de usuarios (Listo)
CREATE PROCEDURE GestionarUsuario 
    @IdUsuario INT = NULL,
    @Cedula VARCHAR(20),
    @Nombre VARCHAR(100),
    @Contrasena VARCHAR(100), -- Contraseña ya hasheada
    @Num_Telefono INT,
    @Direccion TEXT,
    @CorreoElectronico VARCHAR(100),
    @IdRol INT,
    @FechaNacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdUsuario IS NULL
    BEGIN
        -- Validar si el nombre de usuario ya existe antes de insertar
        IF EXISTS (SELECT 1 FROM usuarios WHERE nombre = @Nombre)
        BEGIN
            RAISERROR ('El nombre de usuario ya existe.', 16, 1);
            RETURN;
        END

        -- Inserción de nuevo usuario
        INSERT INTO usuarios (cedula, nombre, contrasena_hash, num_Telefono, direccion, correo_electronico, id_rol, fecha_creacion, ultima_actualizacion)
        VALUES (@Cedula, @Nombre, @Contrasena, @Num_Telefono, @Direccion, @CorreoElectronico, @IdRol, GETDATE(), GETDATE());
    END
    ELSE
    BEGIN
        -- Actualización de usuario existente
        UPDATE usuarios
        SET cedula              = @Cedula,
            nombre              = @Nombre,
            contrasena_hash     = @Contrasena,
            num_Telefono        = @Num_Telefono,
            direccion           = @Direccion,
            correo_electronico  = @CorreoElectronico,
            id_rol              = @IdRol,
            ultima_actualizacion = GETDATE()
        WHERE id_Usuario = @IdUsuario;
    END
END;
GO

-- SP para actualizar datos de niños (Listo)
CREATE PROCEDURE GestionarNino 
    @IdNino INT = NULL,
    @Cedula VARCHAR(20),
    @NombreNino VARCHAR(100),
    @FechaNacimiento DATE,
    @Direccion TEXT,
    @Poliza VARCHAR(100)

AS
BEGIN
    SET NOCOUNT ON;

    IF @IdNino IS NULL
    BEGIN
        -- Inserción de nuevo niño
        INSERT INTO ninos (Cedula, nombre_nino, fecha_nacimiento, direccion, poliza, fecha_creacion, ultima_actualizacion)
        VALUES (@Cedula, @NombreNino, @FechaNacimiento, @Direccion, @Poliza, GETDATE(), GETDATE());
    END
    ELSE
    BEGIN
        -- Actualización de niño existente
        UPDATE ninos
        SET Cedula = @Cedula,
            nombre_nino = @NombreNino,
            fecha_nacimiento = @FechaNacimiento,
            direccion = @Direccion,
            poliza = @Poliza,
            ultima_actualizacion = GETDATE()
        WHERE id_Nino = @IdNino;
    END
END;
GO

-- SP para la actualización de perfil de docentes (Listo)
CREATE PROCEDURE GestionarDocente 
    @IdDocente INT = NULL,
    @IdUsuario INT,
    @FechaNacimiento DATE,
    @GrupoAsignado VARCHAR(100)
AS
BEGIN
    IF @IdDocente IS NULL
    BEGIN
        -- Inserción de nuevo docente
        INSERT INTO docentes (id_usuario, fecha_nacimiento, grupo_asignado, fecha_creacion, ultima_actualizacion)
        VALUES (@IdUsuario, @FechaNacimiento, @GrupoAsignado, GETDATE(), GETDATE());
    END
    ELSE
    BEGIN
        -- Actualización de docente existente
        UPDATE docentes
        SET fecha_nacimiento     = @FechaNacimiento,
            grupo_asignado       = @GrupoAsignado,
            ultima_actualizacion = GETDATE()
        WHERE id_docente = @IdDocente;
    END
END;
GO

-- SP para registrar pagos (Listo)
CREATE PROCEDURE RegistrarPago @IdNino INT,
                               @IdPadre INT,
                               @IdTipoPago INT,
                               @FechaPago DATE,
                               @Monto DECIMAL(10, 2),
                               @MetodoPago VARCHAR(50),
                               @ReferenciaFactura VARCHAR(255)
AS
BEGIN
    INSERT INTO pagos (id_nino, id_padre, id_tipo_pago, fecha_pago, monto, metodo_pago, referencia_factura,
                       fecha_creacion, ultima_actualizacion)
    VALUES (@IdNino, @IdPadre, @IdTipoPago, @FechaPago, @Monto, @MetodoPago, @ReferenciaFactura, GETDATE(), GETDATE());
END;
GO

--  SP para Reporte de Evaluaciones (Listo)
CREATE PROCEDURE ReporteEvaluaciones @IdNino INT = NULL,
                                     @FechaInicio DATE = NULL, 
                                     @FechaFin DATE = NULL 
AS
BEGIN
    SELECT e.id_Evaluacion,
           e.id_nino,
           n.nombre_nino,
           e.asignatura,
           e.puntaje,
           e.fecha,
           e.comentarios
    FROM evaluaciones e
             JOIN ninos n ON e.id_nino = n.id_Nino
    WHERE (@IdNino IS NULL OR e.id_nino = @IdNino)
      AND (@FechaInicio IS NULL OR e.fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR e.fecha <= @FechaFin)
    ORDER BY e.fecha ASC;
END;
GO

-- SP para registrar asistencia de niños (Listo)
CREATE PROCEDURE RegistrarAsistencia @IdNino INT,
                                     @Fecha DATE,
                                     @HoraEntrada TIME,
                                     @HoraSalida TIME,
                                     @Estado VARCHAR(20)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM asistencia WHERE id_nino = @IdNino AND fecha = @Fecha)
        BEGIN
            -- Inserción de asistencia si no existe para esa fecha
            INSERT INTO asistencia (id_nino, fecha, hora_entrada, hora_salida, estado)
            VALUES (@IdNino, @Fecha, @HoraEntrada, @HoraSalida, @Estado);
        END;
END;
GO

-- SP para la gestión de actividades (Listo)
CREATE PROCEDURE RegistrarActividad @IdTipoActividad INT,
                                    @Fecha DATE,
                                    @Lugar VARCHAR(100),
                                    @IdNino INT,
                                    @Asistencia VARCHAR(50)
AS
BEGIN
    DECLARE @IdActividad INT;

    BEGIN TRANSACTION;

    -- Insertar la actividad
    INSERT INTO actividades (id_tipo_actividad, fecha, lugar, activo)
    VALUES (@IdTipoActividad, @Fecha, @Lugar, 1);

    -- Obtener el ID de la actividad recién insertada
    SET @IdActividad = SCOPE_IDENTITY();

    -- Relacionar la actividad con el niño
    INSERT INTO rel_nino_actividad (id_nino, id_actividad, asistencia)
    VALUES (@IdNino, @IdActividad, @Asistencia);

    COMMIT TRANSACTION;
END;
GO

-- SP para gestionar contactos de emergencia de los niños (Listo)
CREATE PROCEDURE GestionarContactoEmergencia @IdContacto INT = NULL,
                                             @NombreContacto VARCHAR(100),
                                             @Relacion VARCHAR(50),
                                             @Telefono VARCHAR(15),
                                             @Direccion VARCHAR(255),
                                             @IdNino INT
AS
BEGIN
    IF @IdContacto IS NULL
        BEGIN
            -- Insertar nuevo contacto de emergencia
            INSERT INTO contactos_emergencia (nombre_contacto, relacion, telefono, direccion, activo)
            VALUES (@NombreContacto, @Relacion, @Telefono, @Direccion, 1);

            -- Relacionar con el niño
            INSERT INTO rel_nino_contacto_emergencia (id_nino, id_contacto)
            VALUES (@IdNino, SCOPE_IDENTITY());
        END
    ELSE
        BEGIN
            -- Actualizar contacto de emergencia existente
            UPDATE contactos_emergencia
            SET nombre_contacto = @NombreContacto,
                relacion        = @Relacion,
                telefono        = @Telefono,
                direccion       = @Direccion,
                activo          = 1
            WHERE id_Contacto_Emergencia = @IdContacto;
        END;
END;
GO

-- SP para Reporte Financiero (Sin Probar)
CREATE PROCEDURE ReporteFinanciero @FechaInicio DATE = NULL,
                                   @FechaFin DATE = NULL,
                                   @IdTipoPago INT = NULL 
AS
BEGIN
    SELECT tp.nombre_tipo_pago,
           SUM(p.monto) AS TotalRecaudado,
           COUNT(p.id_Pago)  AS TotalPagos
    FROM pagos p
             JOIN tipo_pagos tp ON p.id_tipo_pago = tp.id_tipo_pago
    WHERE (@FechaInicio IS NULL OR p.fecha_pago >= @FechaInicio)
      AND (@FechaFin IS NULL OR p.fecha_pago <= @FechaFin)
      AND (@IdTipoPago IS NULL OR p.id_tipo_pago = @IdTipoPago)
    GROUP BY tp.nombre_tipo_pago;
END;
GO

-- SP para Obtener Progreso Académico Histórico (Sin Probar)
CREATE PROCEDURE ObtenerProgresoHistorico @IdNino INT, 
                                          @FechaInicio DATE = NULL, 
                                          @FechaFin DATE = NULL
AS
BEGIN
    SELECT pa.area_academica,
           pa.nivel_progreso,
           pa.descripcion,
           pa.fecha_creacion
    FROM progreso_academico pa
    WHERE pa.id_nino = @IdNino
      AND (@FechaInicio IS NULL OR pa.fecha_creacion >= @FechaInicio)
      AND (@FechaFin IS NULL OR pa.fecha_creacion <= @FechaFin)
    ORDER BY pa.fecha_creacion ASC;
END;
GO

-- SP para Historial de Asistencia de un Niño (Sin Probar)
CREATE PROCEDURE ObtenerHistorialAsistencia @IdNino INT,
                                            @FechaInicio DATE = NULL,
                                            @FechaFin DATE = NULL
AS
BEGIN
    SELECT a.fecha,
           a.hora_entrada,
           a.hora_salida,
           a.estado
    FROM asistencia a
    WHERE a.id_nino = @IdNino
      AND (@FechaInicio IS NULL OR a.fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR a.fecha <= @FechaFin)
    ORDER BY a.fecha ASC;
END;
GO

-- SP para Reporte de Asistencia Agrupada por Mes (Sin Probar)
CREATE PROCEDURE ReporteAsistenciaPorMes @IdNino INT = NULL,
                                         @FechaInicio DATE = NULL,
                                         @FechaFin DATE = NULL
AS
BEGIN
    SELECT YEAR(a.fecha)                                     AS Año,
           MONTH(a.fecha)                                    AS Mes,
           COUNT(CASE WHEN a.estado = 'Presente' THEN 1 END) AS TotalPresente,
           COUNT(CASE WHEN a.estado = 'Ausente' THEN 1 END)  AS TotalAusente,
           COUNT(CASE WHEN a.estado = 'Tarde' THEN 1 END)    AS TotalTarde
    FROM asistencia a
    WHERE (@IdNino IS NULL OR a.id_nino = @IdNino)
      AND (@FechaInicio IS NULL OR a.fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR a.fecha <= @FechaFin)
    GROUP BY YEAR(a.fecha), MONTH(a.fecha)
    ORDER BY Año, Mes;
END;
GO


-- SPs para cambiar estados de los registros (Sin Probar)
CREATE PROCEDURE CambiarEstadoUsuarios @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE usuarios
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id_rol = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoNinos @Id INT,
                                    @Estado BIT
AS
BEGIN
    UPDATE ninos
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id_Nino = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoDocentes @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE docentes
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id_Docente = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoTipoPagos @Id INT,
                                        @Estado BIT
AS
BEGIN
    UPDATE tipo_pagos
    SET activo = @Estado
    WHERE id_tipo_pago = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoActividades @Id INT,
                                          @Estado BIT
AS
BEGIN
    UPDATE actividades
    SET activo = @Estado
    WHERE id_Actividad = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoContactosEmergencia @Id INT,
                                                  @Estado BIT
AS
BEGIN
    UPDATE contactos_emergencia
    SET activo = @Estado
    WHERE id_Contacto_Emergencia = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoAlergias @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE alergias
    SET activo = @Estado
    WHERE id_Alergia = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoCondicionesMedicas @Id INT,
                                                 @Estado BIT
AS
BEGIN
    UPDATE condiciones_medicas
    SET activo = @Estado
    WHERE id_Condicion_medica = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoMedicamentos @Id INT,
                                           @Estado BIT
AS
BEGIN
    UPDATE medicamentos
    SET activo = @Estado
    WHERE id_Medicamento = @Id;
END;
GO