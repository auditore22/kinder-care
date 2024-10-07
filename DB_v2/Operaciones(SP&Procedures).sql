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

-------------------------------------------- VISTAS --------------------------------------------


-- Vista de Usuarios por Rol
CREATE VIEW vw_usuarios_por_rol AS
SELECT u.id_Usuario, u.nombre, r.nombre AS rol
FROM usuarios u
         JOIN roles r ON u.id_rol = r.id_Rol
WHERE u.activo = 1;
GO


-- Vista de Niños por Nombre
CREATE VIEW vw_ninos_por_nombre AS
SELECT id_Nino, cedula, nombre_nino, fecha_nacimiento, direccion, poliza
FROM ninos
WHERE activo = 1;
GO

-- Vista de Padres con sus Usuarios
CREATE VIEW vw_padres_con_usuario AS
SELECT u.id_Usuario AS id_padre, u.nombre AS nombre_usuario
FROM usuarios u
         JOIN rel_padres_ninos rpn ON u.id_Usuario = rpn.id_padre
WHERE u.id_rol = 3
  AND u.activo = 1;
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


-- Vista de Evaluaciones por Niño
CREATE VIEW vw_evaluaciones_por_nino AS
SELECT e.id_Evaluacion, e.id_nino, n.nombre_nino, e.asignatura, e.puntaje, e.fecha, e.comentarios
FROM evaluaciones e
         JOIN ninos n ON e.id_nino = n.id_Nino
WHERE EXISTS (SELECT 1 FROM evaluaciones ee WHERE e.id_nino = ee.id_nino);
GO

-- Vista de Asistencia por Niño
CREATE VIEW vw_asistencia_por_nino AS
SELECT a.id_Asistencia, a.id_nino, n.cedula, n.nombre_nino, a.fecha, a.hora_entrada, a.hora_salida, a.estado
FROM asistencia a
         JOIN ninos n ON a.id_nino = n.id_Nino
WHERE EXISTS (SELECT 1 FROM asistencia aa WHERE a.id_nino = aa.id_nino);
GO

-- Vista de Relación de Padres con Niños
CREATE VIEW vw_rel_padres_ninos AS
SELECT p.id_padre,
       u.id_Usuario AS id_usuario_padre,
       u.nombre      AS nombre_padre,
       p.id_nino,
       n.nombre_nino,
       p.relacion
FROM rel_padres_ninos p
         JOIN usuarios u ON p.id_padre = u.id_Usuario
         JOIN ninos n ON p.id_nino = n.id_Nino
WHERE u.id_rol = 3
  AND EXISTS (SELECT 1 FROM rel_padres_ninos rpn WHERE p.id_nino = rpn.id_nino);
GO



-- Vista de Contactos de Emergencia por Niño
CREATE VIEW vw_contactos_emergencia_por_nino AS
SELECT n.id_Nino AS id_nino,
       n.nombre_nino,
       ce.nombre_contacto,
       ce.relacion,
       ce.telefono,
       ce.direccion
FROM rel_nino_contacto_emergencia rnce
         JOIN ninos n ON rnce.id_nino = n.id_Nino
         JOIN contactos_emergencia ce ON rnce.id_contacto = ce.id_Contacto_Emergencia;
GO

-- Vista para obtener una lista de actividades de un niño
CREATE VIEW vw_actividades_nino AS
SELECT ra.id_nino, a.fecha, ta.nombre_tipo_actividad, ra.asistencia
FROM actividades a
         JOIN rel_nino_actividad ra ON a.id_Actividad = ra.id_actividad
         JOIN tipo_actividad ta ON a.id_tipo_actividad = ta.id_Tipo_Actividad
GO

-- Vista para generar reportes de progreso académico por niño
CREATE VIEW vw_reporte_progreso_academico AS
SELECT pa.id_nino,
       pa.area_academica,
       pa.nivel_progreso,
       pa.descripcion,
       e.asignatura,
       e.puntaje,
       e.fecha        AS FechaEvaluacion,
       od.descripcion AS ObservacionDocente
FROM progreso_academico pa
         LEFT JOIN evaluaciones e ON pa.id_nino = e.id_nino
         LEFT JOIN observaciones_docentes od ON pa.id_nino = od.id_nino;
GO

-- Vista de Niños con Condiciones Médicas
CREATE VIEW vw_ninos_condiciones_medicas AS
SELECT n.id_Nino AS id_nino,
       n.nombre_nino,
       cm.nombre_condicion
FROM rel_nino_condicion rnc
         JOIN ninos n ON rnc.id_nino = n.id_Nino
         JOIN condiciones_medicas cm ON rnc.id_condicion = cm.id_Condicion_medica;
GO

-- Vista de Niños con Alergias
CREATE VIEW vw_ninos_alergias AS
SELECT n.id_Nino AS id_nino,
       n.nombre_nino,
       a.nombre_alergia
FROM rel_nino_alergia rna
         JOIN ninos n ON rna.id_nino = n.id_Nino
         JOIN alergias a ON rna.id_alergia = a.id_Alergia;
GO

-- Vista de Niños con Medicamentos
CREATE VIEW vw_ninos_medicamentos AS
SELECT n.id_Nino AS id_nino,
       n.nombre_nino,
       m.nombre_medicamento,
       m.dosis
FROM rel_nino_medicamento rnm
         JOIN ninos n ON rnm.id_nino = n.id_Nino
         JOIN medicamentos m ON rnm.id_medicamento = m.id_Medicamento;
GO

-- Vista para consultar todos los profesores que enseñan a un niño específico
CREATE VIEW vw_profesores_por_nino AS
SELECT n.id_Nino       AS id_nino,
       n.nombre_nino,
       d.id_Docente       AS id_docente,
       u.nombre   AS nombre_docente,
       rdnm.materia
FROM rel_docente_nino_materia rdnm
         JOIN ninos n ON rdnm.id_nino = n.id_Nino
         JOIN docentes d ON rdnm.id_docente = d.id_Docente
         JOIN usuarios u ON d.id_usuario = u.id_Usuario;
GO


-- Vista para ver todos los niños a los que un docente enseña, y la materia
CREATE VIEW vw_ninos_por_docente AS
SELECT d.id_Docente       AS id_docente,
       u.nombre   AS nombre_docente,
       n.id_Nino       AS id_nino,
       n.nombre_nino,
       rdnm.materia
FROM rel_docente_nino_materia rdnm
         JOIN docentes d ON rdnm.id_docente = d.id_Docente
         JOIN usuarios u ON d.id_usuario = u.id_Usuario
         JOIN ninos n ON rdnm.id_nino = n.id_Nino;
GO


-- Vista expediente
CREATE VIEW vw_expediente AS
SELECT n.id_Nino              AS id_nino,
       n.nombre_nino,

       -- Progreso Académico
       pa.area_academica,
       pa.nivel_progreso,
       pa.descripcion    AS descripcion_progreso,
       pa.fecha_creacion AS fecha_progreso,

       -- Evaluaciones
       e.asignatura,
       e.puntaje,
       e.fecha           AS fecha_evaluacion,
       e.comentarios     AS comentarios_evaluacion,

       -- Observaciones de Docente
       od.descripcion    AS descripcion_observacion,
       od.fecha          AS fecha_observacion,

       -- Historial de Actividades
       a.fecha           AS fecha_actividad,
       ta.nombre_tipo_actividad,
       ra.asistencia

FROM ninos n
         -- Progreso Académico
         LEFT JOIN progreso_academico pa ON n.id_Nino = pa.id_nino

    -- Evaluaciones
         LEFT JOIN evaluaciones e ON n.id_Nino = e.id_nino

    -- Observaciones de Docentes
         LEFT JOIN observaciones_docentes od ON n.id_Nino = od.id_nino

    -- Relación de Actividades y Niños
         LEFT JOIN rel_nino_actividad ra ON n.id_Nino = ra.id_nino
         LEFT JOIN actividades a ON ra.id_actividad = a.id_Actividad
         LEFT JOIN tipo_actividad ta ON a.id_tipo_actividad = ta.id_Tipo_Actividad;
GO


-------------------------------------------- Validar Inserts --------------------------------------------
SELECT 'actividades' AS tabla, COUNT(*) AS total
FROM actividades
UNION ALL
SELECT 'alergias', COUNT(*)
FROM alergias
UNION ALL
SELECT 'asistencia', COUNT(*)
FROM asistencia
UNION ALL
SELECT 'condiciones_medicas', COUNT(*)
FROM condiciones_medicas
UNION ALL
SELECT 'contactos_emergencia', COUNT(*)
FROM contactos_emergencia
UNION ALL
SELECT 'docentes', COUNT(*)
FROM docentes
UNION ALL
SELECT 'evaluaciones', COUNT(*)
FROM evaluaciones
UNION ALL
SELECT 'medicamentos', COUNT(*)
FROM medicamentos
UNION ALL
SELECT 'ninos', COUNT(*)
FROM ninos
UNION ALL
SELECT 'observaciones_docentes', COUNT(*)
FROM observaciones_docentes
UNION ALL
SELECT 'pagos', COUNT(*)
FROM pagos
UNION ALL
SELECT 'progreso_academico', COUNT(*)
FROM progreso_academico
UNION ALL
SELECT 'rel_docente_nino_materia', COUNT(*)
FROM rel_docente_nino_materia
UNION ALL
SELECT 'rel_nino_actividad', COUNT(*)
FROM rel_nino_actividad
UNION ALL
SELECT 'rel_nino_alergia', COUNT(*)
FROM rel_nino_alergia
UNION ALL
SELECT 'rel_nino_condicion', COUNT(*)
FROM rel_nino_condicion
UNION ALL
SELECT 'rel_nino_contacto_emergencia', COUNT(*)
FROM rel_nino_contacto_emergencia
UNION ALL
SELECT 'rel_nino_medicamento', COUNT(*)
FROM rel_nino_medicamento
UNION ALL
SELECT 'rel_padres_ninos', COUNT(*)
FROM rel_padres_ninos
UNION ALL
SELECT 'roles', COUNT(*)
FROM roles
UNION ALL
SELECT 'tipo_actividad', COUNT(*)
FROM tipo_actividad
UNION ALL
SELECT 'tipo_pagos', COUNT(*)
FROM tipo_pagos
UNION ALL
SELECT 'usuarios', COUNT(*)
FROM usuarios;