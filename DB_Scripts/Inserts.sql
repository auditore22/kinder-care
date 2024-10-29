-------------------------------------------- INSERTS --------------------------------------------
-- Insert into Roles
INSERT INTO roles (nombre)
VALUES ('Administrador'), ('Docente'), ('Padre');
GO

-- Insert into Tipo de Pagos
INSERT INTO tipo_pagos (nombre_tipo_pago)
VALUES ('Matrícula'), ('Mensualidad'), ('Excursión'), ('Otro');
GO

-- Insert into Tipo de Actividades
INSERT INTO tipo_actividad (nombre_tipo_actividad)
VALUES ('Deportes'), ('Excursión'), ('Actividad Cultural'), ('Recreativa'), ('Formativa');
GO

-- Insert into Alergias
INSERT INTO alergias (nombre_alergia)
VALUES ('Polen'), ('Leche'), ('Gluten'), ('Cacahuetes'), ('Mariscos'), ('Huevos'), ('Soja'), ('Polvo'), ('Picaduras de insectos');
GO

-- Insert into Condiciones Médicas
INSERT INTO condiciones_medicas (nombre_condicion)
VALUES ('Asma'), ('Diabetes tipo 1'), ('Epilepsia'), ('Déficit de atención e hiperactividad (TDAH)'), ('Alergias alimentarias'),
       ('Dermatitis atópica'), ('Anemia'), ('Infecciones respiratorias recurrentes');
GO

-- Insert into Medicamentos
INSERT INTO medicamentos (nombre_medicamento, dosis)
VALUES ('Paracetamol', '500mg'), ('Ibuprofeno', '200mg'), ('Loratadina', '10mg'), ('Salbutamol', '100mcg'),
       ('Insulina', '1-10u'), ('Amoxicilina', '250mg'), ('Cetirizina', '5mg'), ('Montelukast', '4mg');
GO

-- Insert into Usuarios with roles (Padre, Docente, Administrador)
INSERT INTO usuarios (cedula, nombre, contrasena_hash, num_Telefono, direccion, correo_electronico, id_rol, fecha_creacion, ultima_actualizacion, activo)
VALUES
    ('111111111', 'Juan Morales', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88881111, 'Calle 12, San Jose', 'juan.morales@example.com', 3, GETDATE(), GETDATE(), 1),
    ('222222222', 'Carla Perez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88882222, 'Calle 45, Alajuela', 'carla.perez@example.com', 3, GETDATE(), GETDATE(), 1),
    ('333333333', 'Mario Rodriguez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88883333, 'Avenida Central, Heredia', 'mario.rodriguez@example.com', 3, GETDATE(), GETDATE(), 1),
    ('444444444', 'Ana Jimenez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88884444, 'Barrio Nuevo, Cartago', 'ana.jimenez@example.com', 3, GETDATE(), GETDATE(), 1),
    ('555555555', 'David Sanchez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88885555, 'Calle 30, Liberia', 'david.sanchez@example.com', 3, GETDATE(), GETDATE(), 1),
    ('666666666', 'Lucia Gomez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88886666, 'Calle 50, Escazú', 'lucia.gomez@example.com', 1, GETDATE(), GETDATE(), 1),
    ('777777777', 'Carlos Ruiz', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88887777, 'Calle 65, Guanacaste', 'carlos.ruiz@example.com', 2, GETDATE(), GETDATE(), 1),
    ('888888888', 'Elena Vasquez', 'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==', 88888888, 'Calle 80, Puntarenas', 'elena.vasquez@example.com', 2, GETDATE(), GETDATE(), 1);
GO

-- Insert into Niños
INSERT INTO ninos (cedula, nombre_nino, fecha_nacimiento, direccion, poliza, fecha_creacion, ultima_actualizacion, activo)
VALUES
    ('123456789', 'Carlos Morales', '2015-06-15', 'Calle 12, San Jose', 'Poliza1', GETDATE(), GETDATE(), 1),
    ('987654321', 'Ana Pérez', '2014-09-22', 'Calle 45, Alajuela', 'Poliza2', GETDATE(), GETDATE(), 1),
    ('567890123', 'Luis Rodríguez', '2016-01-10', 'Avenida Central, Heredia', 'Poliza3', GETDATE(), GETDATE(), 1),
    ('234567890', 'Maria Jimenez', '2015-11-03', 'Barrio Nuevo, Cartago', 'Poliza4', GETDATE(), GETDATE(), 1),
    ('345678901', 'David Sánchez', '2013-12-25', 'Calle 30, Liberia', 'Poliza5', GETDATE(), GETDATE(), 1),
    ('456789012', 'Lucia Gomez', '2016-05-15', 'Calle 50, Escazú', 'Poliza6', GETDATE(), GETDATE(), 1),
    ('678901234', 'Carlos Ruiz', '2013-08-09', 'Calle 65, Guanacaste', 'Poliza7', GETDATE(), GETDATE(), 1);
GO

-- Insert into Relación Padres-Niños
INSERT INTO rel_padres_ninos (id_padre, id_nino, relacion)
VALUES
    (1, 1, 'Padre'),
    (1, 2, 'Padre'),
    (2, 3, 'Padre'),
    (3, 4, 'Madre'),
    (4, 5, 'Padre'),
    (5, 6, 'Madre'),
    (6, 7, 'Padre');
GO

-- Insert into Contactos de Emergencia
INSERT INTO contactos_emergencia (nombre_contacto, relacion, telefono, direccion, activo)
VALUES
    ('Jose Morales', 'Tío', 12345678, 'Calle 12, San Jose', 1),
    ('Carla Perez', 'Abuela', 87654321, 'Calle 45, Alajuela', 1),
    ('Mario Rodriguez', 'Tío', 56789012, 'Avenida Central, Heredia', 1),
    ('Ana Jimenez', 'Abuela', 23456789, 'Barrio Nuevo, Cartago', 1),
    ('David Sanchez', 'Hermano', 34567890, 'Calle 30, Liberia', 1),
    ('Lucia Gomez', 'Madre', 45678901, 'Calle 50, Escazú', 1),
    ('Carlos Ruiz', 'Padre', 56789012, 'Calle 65, Guanacaste', 1);
GO

-- Insert into Relación Niño-Contacto de Emergencia
INSERT INTO rel_nino_contacto_emergencia (id_nino, id_contacto)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (7, 7);
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
