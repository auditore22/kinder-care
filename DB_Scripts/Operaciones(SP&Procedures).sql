-------------------------------------------- SPs --------------------------------------------
-- SP para la creación de usuarios (Listo)
CREATE PROCEDURE GestionarUsuario @IdUsuario INT = NULL,
                                  @Cedula VARCHAR(20),
                                  @Nombre VARCHAR(100),
                                  @Contrasena VARCHAR(100), -- Contraseña ya hasheada
                                  @Num_Telefono INT,
                                  @Direccion VARCHAR(256),
                                  @CorreoElectronico VARCHAR(100),
                                  @IdRol INT
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdUsuario IS NULL
        BEGIN
            -- Validar si el nombre de usuario ya existe antes de insertar
            IF EXISTS (SELECT 1 FROM usuarios WHERE nombre = @Nombre)
                BEGIN
                    RAISERROR ('El nombre de usuario ya existe.', 16, 1);
                END

            -- Inserción de nuevo usuario
            INSERT INTO usuarios (cedula, nombre, contrasena_hash, num_Telefono, direccion, correo_electronico, id_rol,
                                  fecha_creacion, ultima_actualizacion)
            VALUES (@Cedula, @Nombre, @Contrasena, @Num_Telefono, @Direccion, @CorreoElectronico, @IdRol, GETDATE(),
                    GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de usuario existente
            UPDATE usuarios
            SET cedula               = @Cedula,
                nombre               = @Nombre,
                contrasena_hash      = @Contrasena,
                num_Telefono         = @Num_Telefono,
                direccion            = @Direccion,
                correo_electronico   = @CorreoElectronico,
                id_rol               = @IdRol,
                ultima_actualizacion = GETDATE()
            WHERE id_Usuario = @IdUsuario;
        END
END;
GO

-- SP para Gestionar Tareas de niños (Listo)
CREATE PROCEDURE GestionarTareas @id_nino INT = NULL,
                                 @id_tarea INT = NULL,
                                 @id_profesor INT = NULL,
                                 @nombre NVARCHAR(255) = NULL,
                                 @descripcion NVARCHAR(MAX) = NULL,
                                 @calificacion INT = NULL,
                                 @fecha_asignada DATE = NULL,
                                 @fecha_entrega DATE = NULL,
                                 @activo BIT = 1,
                                 @accion NVARCHAR(10) = 'AGREGAR'
AS
BEGIN
    IF @accion = 'AGREGAR'
        BEGIN
            IF @id_profesor IS NULL OR @nombre IS NULL OR @fecha_entrega IS NULL
                BEGIN
                    RAISERROR ('Debe proporcionar id_profesor, nombre y fecha_entrega para agregar una nueva tarea.', 16, 1);
                    RETURN;
                END

            -- Insertar la nueva tarea
            INSERT INTO Tareas (id_profesor, nombre, descripcion, calificacion, fecha_asignada, fecha_entrega, activo)
            VALUES (@id_profesor, @nombre, @descripcion, ISNULL(@calificacion, 0), ISNULL(@fecha_asignada, GETDATE()),
                    @fecha_entrega, @activo);

            -- Obtener el id_tarea de la tarea recién insertada
            SET @id_tarea = SCOPE_IDENTITY();

            -- Insertar en la tabla rel_nino_tarea
            INSERT INTO rel_nino_tarea(id_nino, id_tarea) VALUES (@id_nino, @id_tarea);

            RETURN;
        END
    ELSE
        IF @accion = 'ACTUALIZAR' AND @id_tarea IS NOT NULL
            BEGIN
                IF @id_profesor IS NULL OR @nombre IS NULL OR @fecha_entrega IS NULL
                    BEGIN
                        RAISERROR ('Debe proporcionar id_profesor, nombre y fecha_entrega para actualizar una tarea.', 16, 1);
                        RETURN;
                    END

                UPDATE Tareas
                SET id_profesor    = @id_profesor,
                    nombre         = @nombre,
                    descripcion    = @descripcion,
                    calificacion   = ISNULL(@calificacion, 0),
                    fecha_asignada = ISNULL(@fecha_asignada, GETDATE()),
                    fecha_entrega  = @fecha_entrega,
                    activo         = @activo
                WHERE id_tarea = @id_tarea;

                RETURN;
            END
        ELSE
            IF @accion = 'ELIMINAR' AND @id_tarea IS NOT NULL
                BEGIN
                    -- Eliminación lógica: marcar el registro como inactivo
                    UPDATE Tareas
                    SET activo = 0
                    WHERE id_tarea = @id_tarea;

                    DELETE FROM rel_nino_tarea WHERE id_tarea = @id_tarea AND id_nino = @id_nino;

                    RETURN;
                END
            ELSE
                BEGIN
                    RAISERROR ('Acción no válida o parámetros incompletos.', 16, 1);
                    RETURN;
                END
END;
GO
  
-- SP para actualizar datos de niños (Listo)
CREATE PROCEDURE GestionarNino @IdNino INT = NULL,
                               @Cedula VARCHAR(20),
                               @NombreNino VARCHAR(100),
                               @FechaNacimiento DATE,
                               @Direccion VARCHAR(256),
                               @Poliza VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdNino IS NULL
        BEGIN
            -- Inserción de nuevo niño
            INSERT INTO ninos (Cedula, nombre_nino, fecha_nacimiento, direccion, poliza, fecha_creacion,
                               ultima_actualizacion)
            VALUES (@Cedula, @NombreNino, @FechaNacimiento, @Direccion, @Poliza, GETDATE(), GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de niño existente
            UPDATE ninos
            SET Cedula               = @Cedula,
                nombre_nino          = @NombreNino,
                fecha_nacimiento     = @FechaNacimiento,
                direccion            = @Direccion,
                poliza               = @Poliza,
                ultima_actualizacion = GETDATE()
            WHERE id_Nino = @IdNino;
        END
END;
GO

-- SP para la actualización de perfil de docentes (Listo)
CREATE PROCEDURE GestionarDocente @IdDocente INT = NULL,
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
    ORDER BY e.fecha;
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


CREATE PROCEDURE GestionarInformacionMedicaNino @id_nino INT,
                                                @id_alergia INT = NULL,
                                                @id_condicion INT = NULL,
                                                @id_medicamento INT = NULL,
                                                @accion VARCHAR(10) = 'AGREGAR'
AS
BEGIN
    IF @accion = 'AGREGAR'
        BEGIN
            IF @id_alergia IS NOT NULL
                INSERT INTO rel_nino_alergia (id_nino, id_alergia)
                VALUES (@id_nino, @id_alergia);

            IF @id_condicion IS NOT NULL
                INSERT INTO rel_nino_condicion (id_nino, id_condicion)
                VALUES (@id_nino, @id_condicion);

            IF @id_medicamento IS NOT NULL
                INSERT INTO rel_nino_medicamento (id_nino, id_medicamento)
                VALUES (@id_nino, @id_medicamento);
        END
    ELSE
        IF @accion = 'ELIMINAR'
            BEGIN
                IF @id_alergia IS NOT NULL
                    DELETE
                    FROM rel_nino_alergia
                    WHERE id_nino = @id_nino
                      AND id_alergia = @id_alergia;

                IF @id_condicion IS NOT NULL
                    DELETE
                    FROM rel_nino_condicion
                    WHERE id_nino = @id_nino
                      AND id_condicion = @id_condicion;

                IF @id_medicamento IS NOT NULL
                    DELETE
                    FROM rel_nino_medicamento
                    WHERE id_nino = @id_nino
                      AND id_medicamento = @id_medicamento;
            END
END;
GO

CREATE PROCEDURE GestionarContactosEmergencia @id_nino INT,
                                              @nombre_contacto VARCHAR(100) = NULL,
                                              @telefono INT = NULL,
                                              @relacion VARCHAR(50) = NULL,
                                              @id_contacto INT = NULL,
                                              @accion VARCHAR(10) = 'AGREGAR'
AS
BEGIN
    IF @accion = 'AGREGAR'
        BEGIN
            IF @nombre_contacto IS NULL OR @telefono IS NULL OR @relacion IS NULL
                BEGIN
                    RAISERROR ('Debe proporcionar nombre_contacto, telefono y relacion para agregar un nuevo contacto.', 16, 1);
                    RETURN;
                END

            INSERT INTO contactos_emergencia (nombre_contacto, telefono, relacion)
            VALUES (@nombre_contacto, @telefono, @relacion);

            DECLARE @nuevo_id_contacto INT = SCOPE_IDENTITY();
            INSERT INTO rel_nino_contacto_emergencia (id_nino, id_contacto)
            VALUES (@id_nino, @nuevo_id_contacto);
        END
    ELSE
        IF @accion = 'ACTUALIZAR' AND @id_contacto IS NOT NULL
            BEGIN
                IF @nombre_contacto IS NULL OR @telefono IS NULL OR @relacion IS NULL
                    BEGIN
                        RAISERROR ('Debe proporcionar nombre_contacto, telefono y relacion para actualizar un contacto.', 16, 1);
                        RETURN;
                    END

                UPDATE contactos_emergencia
                SET nombre_contacto = @nombre_contacto,
                    telefono        = @telefono,
                    relacion        = @relacion
                WHERE id_Contacto_Emergencia = @id_contacto;
            END
        ELSE
            IF @accion = 'ELIMINAR' AND @id_contacto IS NOT NULL
                BEGIN
                    DELETE
                    FROM rel_nino_contacto_emergencia
                    WHERE id_contacto = @id_contacto
                      AND id_nino = @id_nino;
                END
            ELSE
                BEGIN
                    RAISERROR ('Acción no válida o parámetros incompletos.', 16, 1);
                END
END;
GO

CREATE PROCEDURE sp_ConsultarExpedienteNino
@id_nino INT
AS
BEGIN
    SELECT
        n.id_Nino,
        n.cedula,
        n.nombre_nino,
        n.fecha_nacimiento,
        n.direccion,
        n.poliza,
        STRING_AGG(a.nombre_alergia, ', ') AS nombre_alergia,
        STRING_AGG(c.nombre_condicion, ', ') AS nombre_condicion,
        STRING_AGG(m.nombre_medicamento, ', ') AS nombre_medicamento,
        STRING_AGG(m.dosis, ', ') AS dosis,
        STRING_AGG(ce.nombre_contacto, ', ') AS nombre_contacto,
        STRING_AGG(ce.telefono, ', ') AS telefono_contacto,
        STRING_AGG(ce.relacion, ', ') AS relacion_contacto
    FROM ninos n
             LEFT JOIN rel_nino_alergia ra ON n.id_Nino = ra.id_nino
             LEFT JOIN alergias a ON ra.id_alergia = a.id_Alergia
             LEFT JOIN rel_nino_condicion rc ON n.id_Nino = rc.id_nino
             LEFT JOIN condiciones_medicas c ON rc.id_condicion = c.id_Condicion_medica
             LEFT JOIN rel_nino_medicamento rm ON n.id_Nino = rm.id_nino
             LEFT JOIN medicamentos m ON rm.id_medicamento = m.id_Medicamento
             LEFT JOIN rel_nino_contacto_emergencia rce ON n.id_Nino = rce.id_nino
             LEFT JOIN contactos_emergencia ce ON rce.id_contacto = ce.id_Contacto_Emergencia
    WHERE n.id_Nino = @id_nino
    GROUP BY
        n.id_Nino,
        n.cedula,
        n.nombre_nino,
        n.fecha_nacimiento,
        n.direccion,
        n.poliza;
END;
GO

CREATE PROCEDURE sp_ActualizarExpedienteNino
    @id_nino INT,
    @nombre_nino VARCHAR(100),
    @direccion VARCHAR(256),
    @poliza VARCHAR(100)
AS
BEGIN
    UPDATE ninos
    SET nombre_nino = @nombre_nino,
        direccion = @direccion,
        poliza = @poliza,
        ultima_actualizacion = GETDATE()
    WHERE id_Nino = @id_nino;
END;
GO

CREATE PROCEDURE UsuariosActualizar
    @id_Usuario INT,
    @nombre NVARCHAR(100),
    @direccion NVARCHAR(200),
    @correo_electronico NVARCHAR(100),
    @activo BIT,
    @id_rol INT
AS
BEGIN
    UPDATE Usuarios
    SET 
        nombre = @nombre,
        @direccion = @direccion,
        correo_electronico = @correo_electronico,
        activo = @activo,
        id_rol = @id_rol
    WHERE id_Usuario = @id_Usuario
END;
GO
  
CREATE PROCEDURE DocentesActualizar
    @id_docente INT,
    @id_usuario INT,
    @nombre VARCHAR(100),
    @correo_electronico VARCHAR(100),
    @direccion VARCHAR(256),
    @activo BIT,
    @fecha_nacimiento DATE,
    @grupo_asignado VARCHAR(50),
    @num_Telefono VARCHAR(15)  
AS
BEGIN

    UPDATE usuarios
    SET 
        nombre = @nombre,
        correo_electronico = @correo_electronico,
        direccion = @direccion,
        num_Telefono = @num_Telefono,  
        activo = @activo,
        ultima_actualizacion = GETDATE()
    WHERE id_usuario = @id_usuario;

    UPDATE docentes
    SET 
        fecha_nacimiento = @fecha_nacimiento, 
        grupo_asignado = @grupo_asignado,
        ultima_actualizacion = GETDATE()
    WHERE id_docente = @id_docente;

END;
GO
  
CREATE PROCEDURE ObtenerAsistenciaDiaria
    @Fecha DATE
AS
BEGIN
    SELECT 
        a.id_asistencia,
        n.nombre_nino,
        a.hora_entrada,
        a.hora_salida,
        a.estado
    FROM asistencia a
    INNER JOIN ninos n ON a.id_nino = n.id_nino
    WHERE a.fecha = @Fecha;
END;
GO
  
-------------------------------------------- VISTAS --------------------------------------------


CREATE VIEW vw_ExpedienteCompletoNino AS
SELECT n.id_Nino,
       n.cedula,
       n.nombre_nino,
       n.fecha_nacimiento,
       n.direccion,
       n.poliza,
       STRING_AGG(a.nombre_alergia, ', ')     AS nombre_alergia,
       STRING_AGG(c.nombre_condicion, ', ')   AS nombre_condicion,
       STRING_AGG(m.nombre_medicamento, ', ') AS nombre_medicamento,
       STRING_AGG(m.dosis, ', ')              AS dosis,
       STRING_AGG(ce.nombre_contacto, ', ')   AS nombre_contacto,
       STRING_AGG(ce.telefono, ', ')          AS telefono_contacto,
       STRING_AGG(ce.relacion, ', ')          AS relacion_contacto
FROM ninos n
         LEFT JOIN rel_nino_alergia ra ON n.id_Nino = ra.id_nino
         LEFT JOIN alergias a ON ra.id_alergia = a.id_Alergia
         LEFT JOIN rel_nino_condicion rc ON n.id_Nino = rc.id_nino
         LEFT JOIN condiciones_medicas c ON rc.id_condicion = c.id_Condicion_medica
         LEFT JOIN rel_nino_medicamento rm ON n.id_Nino = rm.id_nino
         LEFT JOIN medicamentos m ON rm.id_medicamento = m.id_Medicamento
         LEFT JOIN rel_nino_contacto_emergencia rce ON n.id_Nino = rce.id_nino
         LEFT JOIN contactos_emergencia ce ON rce.id_contacto = ce.id_Contacto_Emergencia
GROUP BY n.id_Nino,
         n.cedula,
         n.nombre_nino,
         n.fecha_nacimiento,
         n.direccion,
         n.poliza;
GO

-- Vista de Pagos por Padre
CREATE VIEW vw_pagos_por_padre AS
SELECT p.id_Pago,
       p.id_nino,
       n.nombre_nino,
       p.id_padre,
       u.nombre AS nombre_padre,
       p.monto,
       tp.nombre_tipo_pago,
       p.metodo_pago,
       p.referencia_factura,
       p.fecha_pago
FROM pagos p
         JOIN tipo_pagos tp ON p.id_tipo_pago = tp.id_tipo_pago
         JOIN ninos n ON p.id_nino = n.id_Nino
         JOIN usuarios u ON p.id_padre = u.id_Usuario
WHERE u.id_rol = 3
  AND EXISTS (SELECT 1 FROM pagos pp WHERE p.id_padre = pp.id_padre);
GO
