-------------------------------------------- INSERTS --------------------------------------------
INSERT INTO roles (nombre)
VALUES ('Administrador'),
       ('Docente'),
       ('Padre');

INSERT INTO tipo_pagos (nombre_tipo_pago)
VALUES ('Matrícula'),
       ('Mensualidad'),
       ('Excursión');

INSERT INTO tipo_actividad (nombre_tipo_actividad)
VALUES ('Deportes'),
       ('Excursión'),
       ('Actividad Cultural');

INSERT INTO alergias (nombre_alergia)
VALUES ('Polen'),
       ('Leche'),
       ('Gluten'),
       ('Cacahuetes'),
       ('Mariscos'),
       ('Huevos'),
       ('Soja'),
       ('Polvo'),
       ('Picaduras de insectos');


INSERT INTO condiciones_medicas (nombre_condicion)
VALUES ('Asma'),
       ('Diabetes tipo 1'),
       ('Epilepsia'),
       ('Déficit de atención e hiperactividad (TDAH)'),
       ('Alergias alimentarias'),
       ('Dermatitis atópica'),
       ('Anemia'),
       ('Infecciones respiratorias recurrentes');


INSERT INTO medicamentos (nombre_medicamento, dosis)
VALUES ('Paracetamol', '500mg'),
       ('Ibuprofeno', '200mg'),
       ('Loratadina', '10mg'),
       ('Salbutamol', '100mcg'),
       ('Insulina', '1-10u'),
       ('Amoxicilina', '250mg'),
       ('Cetirizina', '5mg'),
       ('Montelukast', '4mg');
GO

GO
-------------------------------------------- SPs --------------------------------------------
-- SP para la creación de usuarios
CREATE PROCEDURE GestionarUsuario @IdUsuario INT = NULL,
                                  @Nombre VARCHAR(100),
                                  @ContrasenaHash VARCHAR(100),
                                  @IdRol INT
AS
BEGIN
    -- Validar si el nombre de usuario ya existe antes de insertar
    IF @IdUsuario IS NULL
        BEGIN
            IF EXISTS (SELECT 1 FROM usuarios WHERE nombre = @Nombre)
                BEGIN
                    RAISERROR ('El nombre de usuario ya existe.', 16, 1);
                    RETURN;
                END
            -- Inserción de nuevo usuario
            INSERT INTO usuarios (nombre, contrasena_hash, id_rol, fecha_creacion, ultima_actualizacion)
            VALUES (@Nombre, @ContrasenaHash, @IdRol, GETDATE(), GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de usuario existente
            UPDATE usuarios
            SET nombre               = @Nombre,
                contrasena_hash      = @ContrasenaHash,
                id_rol               = @IdRol,
                ultima_actualizacion = GETDATE()
            WHERE id = @IdUsuario;
        END
END;
GO


-- SP para actualizar datos de niños
CREATE PROCEDURE GestionarNino @IdNino INT = NULL,
                               @NombreNino VARCHAR(100),
                               @FechaNacimiento DATE,
                               @Direccion TEXT,
                               @Poliza VARCHAR(100)
AS
BEGIN
    IF @IdNino IS NULL
        BEGIN
            -- Inserción de nuevo niño
            INSERT INTO ninos (nombre_nino, fecha_nacimiento, direccion, poliza, fecha_creacion, ultima_actualizacion)
            VALUES (@NombreNino, @FechaNacimiento, @Direccion, @Poliza, GETDATE(), GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de niño existente
            UPDATE ninos
            SET nombre_nino          = @NombreNino,
                fecha_nacimiento     = @FechaNacimiento,
                direccion            = @Direccion,
                poliza               = @Poliza,
                ultima_actualizacion = GETDATE()
            WHERE id = @IdNino;
        END
END;
GO


-- SP para la actualización de perfil de docentes
CREATE PROCEDURE GestionarDocente @IdDocente INT = NULL,
                                  @IdUsuario INT,
                                  @Nombre VARCHAR(100),
                                  @Apellido VARCHAR(100),
                                  @FechaNacimiento DATE,
                                  @Direccion TEXT,
                                  @Telefono VARCHAR(15),
                                  @CorreoElectronico VARCHAR(100),
                                  @GrupoAsignado VARCHAR(100)
AS
BEGIN
    IF @IdDocente IS NULL
        BEGIN
            -- Inserción de nuevo docente
            INSERT INTO docentes (id_usuario, nombre, apellido, fecha_nacimiento, direccion, telefono,
                                  correo_electronico, grupo_asignado, fecha_creacion, ultima_actualizacion)
            VALUES (@IdUsuario, @Nombre, @Apellido, @FechaNacimiento, @Direccion, @Telefono, @CorreoElectronico,
                    @GrupoAsignado, GETDATE(), GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de docente existente
            UPDATE docentes
            SET nombre               = @Nombre,
                apellido             = @Apellido,
                fecha_nacimiento     = @FechaNacimiento,
                direccion            = @Direccion,
                telefono             = @Telefono,
                correo_electronico   = @CorreoElectronico,
                grupo_asignado       = @GrupoAsignado,
                ultima_actualizacion = GETDATE()
            WHERE id = @IdDocente;
        END
END;
GO


-- SP para manejar inserción y modificación de padres
CREATE PROCEDURE GestionarPadre @IdPadre INT = NULL,
                                @IdUsuario INT
AS
BEGIN
    IF @IdPadre IS NULL
        BEGIN
            -- Inserción de nuevo padre
            INSERT INTO padres (id_usuario, fecha_creacion, ultima_actualizacion)
            VALUES (@IdUsuario, GETDATE(), GETDATE());
        END
    ELSE
        BEGIN
            -- Actualización de padre existente
            UPDATE padres
            SET id_usuario           = @IdUsuario,
                ultima_actualizacion = GETDATE()
            WHERE id = @IdPadre;
        END
END;
GO


-- SP para registrar asistencia de niños
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


--  SP para Reporte de Evaluaciones
CREATE PROCEDURE ReporteEvaluaciones @IdNino INT = NULL,
                                     @FechaInicio DATE = NULL, 
                                     @FechaFin DATE = NULL 
AS
BEGIN
    SELECT e.id,
           e.id_nino,
           n.nombre_nino,
           e.asignatura,
           e.puntaje,
           e.fecha,
           e.comentarios
    FROM evaluaciones e
             JOIN ninos n ON e.id_nino = n.id
    WHERE (@IdNino IS NULL OR e.id_nino = @IdNino)
      AND (@FechaInicio IS NULL OR e.fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR e.fecha <= @FechaFin)
    ORDER BY e.fecha ASC;
END;
GO


-- SP para registrar pagos
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


-- SP para la gestión de actividades
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

-- SP para gestionar contactos de emergencia de los niños
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
            WHERE id = @IdContacto;
        END;
END;
GO

-- SP para Reporte Financiero
CREATE PROCEDURE ReporteFinanciero @FechaInicio DATE = NULL,
                                   @FechaFin DATE = NULL,
                                   @IdTipoPago INT = NULL 
AS
BEGIN
    SELECT tp.nombre_tipo_pago,
           SUM(p.monto) AS TotalRecaudado,
           COUNT(p.id)  AS TotalPagos
    FROM pagos p
             JOIN tipo_pagos tp ON p.id_tipo_pago = tp.id
    WHERE (@FechaInicio IS NULL OR p.fecha_pago >= @FechaInicio)
      AND (@FechaFin IS NULL OR p.fecha_pago <= @FechaFin)
      AND (@IdTipoPago IS NULL OR p.id_tipo_pago = @IdTipoPago)
    GROUP BY tp.nombre_tipo_pago;
END;
GO

-- SP para Obtener Progreso Académico Histórico
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

-- SP para Historial de Asistencia de un Niño
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

-- SP para Reporte de Asistencia Agrupada por Mes
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


-- SPs para cambiar estados de los registros
CREATE PROCEDURE CambiarEstadoUsuarios @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE usuarios
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoNinos @Id INT,
                                    @Estado BIT
AS
BEGIN
    UPDATE ninos
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoPadres @Id INT,
                                     @Estado BIT
AS
BEGIN
    UPDATE padres
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoDocentes @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE docentes
    SET activo               = @Estado,
        ultima_actualizacion = GETDATE()
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoTipoPagos @Id INT,
                                        @Estado BIT
AS
BEGIN
    UPDATE tipo_pagos
    SET activo = @Estado
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoActividades @Id INT,
                                          @Estado BIT
AS
BEGIN
    UPDATE actividades
    SET activo = @Estado
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoContactosEmergencia @Id INT,
                                                  @Estado BIT
AS
BEGIN
    UPDATE contactos_emergencia
    SET activo = @Estado
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoAlergias @Id INT,
                                       @Estado BIT
AS
BEGIN
    UPDATE alergias
    SET activo = @Estado
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoCondicionesMedicas @Id INT,
                                                 @Estado BIT
AS
BEGIN
    UPDATE condiciones_medicas
    SET activo = @Estado
    WHERE id = @Id;
END;
GO

CREATE PROCEDURE CambiarEstadoMedicamentos @Id INT,
                                           @Estado BIT
AS
BEGIN
    UPDATE medicamentos
    SET activo = @Estado
    WHERE id = @Id;
END;
GO
-------------------------------------------- VISTAS --------------------------------------------


-- Vista de Usuarios por Rol
CREATE VIEW vw_usuarios_por_rol AS
SELECT u.id, u.nombre, r.nombre AS rol
FROM usuarios u
         JOIN roles r ON u.id_rol = r.id
WHERE u.activo = 1;
GO

-- Vista de Niños por Nombre
CREATE VIEW vw_ninos_por_nombre AS
SELECT id, nombre_nino, fecha_nacimiento, direccion, poliza
FROM ninos
WHERE activo = 1;
GO

-- Vista de Padres con sus Usuarios
CREATE VIEW vw_padres_con_usuario AS
SELECT p.id, u.nombre AS nombre_usuario
FROM padres p
         JOIN usuarios u ON p.id_usuario = u.id
WHERE p.activo = 1
  AND u.activo = 1;
GO

-- Vista de Pagos por Padre
CREATE VIEW vw_pagos_por_padre AS
SELECT p.id,
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
         JOIN tipo_pagos tp ON p.id_tipo_pago = tp.id
         JOIN ninos n ON p.id_nino = n.id
         JOIN padres pa ON p.id_padre = pa.id
         JOIN usuarios u ON pa.id_usuario = u.id
WHERE EXISTS (SELECT 1 FROM pagos pp WHERE p.id_padre = pp.id_padre);
GO

-- Vista de Evaluaciones por Niño
CREATE VIEW vw_evaluaciones_por_nino AS
SELECT e.id, e.id_nino, n.nombre_nino, e.asignatura, e.puntaje, e.fecha, e.comentarios
FROM evaluaciones e
         JOIN ninos n ON e.id_nino = n.id
WHERE EXISTS (SELECT 1 FROM evaluaciones ee WHERE e.id_nino = ee.id_nino);
GO

-- Vista de Asistencia por Niño
CREATE VIEW vw_asistencia_por_nino AS
SELECT a.id, a.id_nino, n.nombre_nino, a.fecha, a.hora_entrada, a.hora_salida, a.estado
FROM asistencia a
         JOIN ninos n ON a.id_nino = n.id
WHERE EXISTS (SELECT 1 FROM asistencia aa WHERE a.id_nino = aa.id_nino);
GO

-- Vista de Relación de Padres con Niños
CREATE VIEW vw_rel_padres_ninos AS
SELECT p.id_padre,
       pa.id_usuario AS id_usuario_padre,
       u.nombre      AS nombre_padre,
       p.id_nino,
       n.nombre_nino,
       p.relacion
FROM rel_padres_ninos p
         JOIN padres pa ON p.id_padre = pa.id
         JOIN usuarios u ON pa.id_usuario = u.id
         JOIN ninos n ON p.id_nino = n.id
WHERE EXISTS (SELECT 1 FROM rel_padres_ninos rpn WHERE p.id_nino = rpn.id_nino);
GO



-- Vista de Contactos de Emergencia por Niño
CREATE VIEW vw_contactos_emergencia_por_nino AS
SELECT n.id AS id_nino,
       n.nombre_nino,
       ce.nombre_contacto,
       ce.relacion,
       ce.telefono,
       ce.direccion
FROM rel_nino_contacto_emergencia rnce
         JOIN ninos n ON rnce.id_nino = n.id
         JOIN contactos_emergencia ce ON rnce.id_contacto = ce.id;
GO

-- Vista para obtener una lista de actividades de un niño
CREATE VIEW vw_actividades_nino AS
SELECT ra.id_nino, a.fecha, ta.nombre_tipo_actividad, ra.asistencia
FROM actividades a
         JOIN rel_nino_actividad ra ON a.id = ra.id_actividad
         JOIN tipo_actividad ta ON a.id_tipo_actividad = ta.id
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
SELECT n.id AS id_nino,
       n.nombre_nino,
       cm.nombre_condicion
FROM rel_nino_condicion rnc
         JOIN ninos n ON rnc.id_nino = n.id
         JOIN condiciones_medicas cm ON rnc.id_condicion = cm.id;
GO

-- Vista de Niños con Alergias
CREATE VIEW vw_ninos_alergias AS
SELECT n.id AS id_nino,
       n.nombre_nino,
       a.nombre_alergia
FROM rel_nino_alergia rna
         JOIN ninos n ON rna.id_nino = n.id
         JOIN alergias a ON rna.id_alergia = a.id;
GO

-- Vista de Niños con Medicamentos
CREATE VIEW vw_ninos_medicamentos AS
SELECT n.id AS id_nino,
       n.nombre_nino,
       m.nombre_medicamento,
       m.dosis
FROM rel_nino_medicamento rnm
         JOIN ninos n ON rnm.id_nino = n.id
         JOIN medicamentos m ON rnm.id_medicamento = m.id;
GO

-- Vista para consultar todos los profesores que enseñan a un niño específico
CREATE VIEW vw_profesores_por_nino AS
SELECT n.id       AS id_nino,
       n.nombre_nino,
       d.id       AS id_docente,
       d.nombre   AS nombre_docente,
       d.apellido AS apellido_docente,
       rdnm.materia
FROM rel_docente_nino_materia rdnm
         JOIN ninos n ON rdnm.id_nino = n.id
         JOIN docentes d ON rdnm.id_docente = d.id;
GO

-- Vista para ver todos los niños a los que un docente enseña, y la materia
CREATE VIEW vw_ninos_por_docente AS
SELECT d.id       AS id_docente,
       d.nombre   AS nombre_docente,
       d.apellido AS apellido_docente,
       n.id       AS id_nino,
       n.nombre_nino,
       rdnm.materia
FROM rel_docente_nino_materia rdnm
         JOIN docentes d ON rdnm.id_docente = d.id
         JOIN ninos n ON rdnm.id_nino = n.id;
GO

-- Vista expediente
CREATE VIEW vw_expediente AS
SELECT n.id              AS id_nino,
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
         LEFT JOIN progreso_academico pa ON n.id = pa.id_nino

    -- Evaluaciones
         LEFT JOIN evaluaciones e ON n.id = e.id_nino

    -- Observaciones de Docentes
         LEFT JOIN observaciones_docentes od ON n.id = od.id_nino

    -- Relación de Actividades y Niños
         LEFT JOIN rel_nino_actividad ra ON n.id = ra.id_nino
         LEFT JOIN actividades a ON ra.id_actividad = a.id
         LEFT JOIN tipo_actividad ta ON a.id_tipo_actividad = ta.id;
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
SELECT 'padres', COUNT(*)
FROM padres
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