-------------------------------------------- INSERTS --------------------------------------------
-- Insertar registros en tabla Roles
INSERT INTO roles (nombre)
VALUES ('Administrador'), ('Docente'), ('Padre');
GO

-- Insertar registros en Tipo de Pagos
INSERT INTO tipo_pagos (nombre_tipo_pago)
VALUES ('Matrícula'), ('Mensualidad'), ('Excursión'), ('Otro');
GO

-- Insertar registros en Tipo de Actividades
INSERT INTO tipo_actividad (nombre_tipo_actividad)
VALUES ('Deportes'), ('Excursión'), ('Actividad Cultural'), ('Recreativa'), ('Formativa');
GO

-- Insertar registros en Alergias
INSERT INTO alergias (nombre_alergia)
VALUES ('Polen'), ('Leche'), ('Gluten'), ('Cacahuetes'), ('Mariscos'), ('Huevos'), ('Soja'), ('Polvo'), ('Picaduras de insectos');
GO

-- Insertar registros en Condiciones Médicas
INSERT INTO condiciones_medicas (nombre_condicion)
VALUES ('Asma'), ('Diabetes tipo 1'), ('Epilepsia'), ('Déficit de atención e hiperactividad (TDAH)'), ('Alergias alimentarias'),
       ('Dermatitis atópica'), ('Anemia'), ('Infecciones respiratorias recurrentes');
GO

-- Insertar registros en Medicamentos
INSERT INTO medicamentos (nombre_medicamento, dosis)
VALUES ('Paracetamol', '500mg'), ('Ibuprofeno', '200mg'), ('Loratadina', '10mg'), ('Salbutamol', '100mcg'),
       ('Insulina', '1-10u'), ('Amoxicilina', '250mg'), ('Cetirizina', '5mg'), ('Montelukast', '4mg');
GO

-- Insertar registros en Usuarios con roles (Padre, Docente, Administrador)
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

-- Insertar registros en Niños
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

-- Insertar registros en la tabla actividades
INSERT INTO actividades (id_tipo_actividad, fecha, lugar, descripcion, activo)
VALUES
    -- Noviembre
    (1, '2024-11-01T09:00:00', 'Cancha Principal', 'Partido amistoso de fútbol', 1),
    (2, '2024-11-03T14:00:00', 'Parque Central', 'Excursión al parque', 1),
    (3, '2024-11-05T10:00:00', 'Teatro Nacional', 'Obra de teatro cultural', 1),
    (4, '2024-11-06T08:30:00', 'Sala de Juegos', 'Actividad recreativa con juegos', 1),
    (5, '2024-11-08T13:00:00', 'Salón de Clases 3A', 'Taller formativo sobre valores', 1),
    (1, '2024-11-10T11:00:00', 'Gimnasio', 'Torneo de baloncesto', 1),
    (2, '2024-11-11T15:00:00', 'Playa Hermosa', 'Excursión a la playa', 1),
    (3, '2024-11-12T09:30:00', 'Museo Nacional', 'Visita guiada cultural', 1),
    (4, '2024-11-14T14:30:00', 'Área Verde', 'Juegos al aire libre', 1),
    (5, '2024-11-15T10:30:00', 'Salón de Actos', 'Charla formativa sobre nutrición', 1),
    (1, '2024-11-17T08:00:00', 'Piscina', 'Competencia de natación', 1),
    (4, '2024-11-17T11:00:00', 'Sala de Música', 'Taller de instrumentos', 1),
    (2, '2024-11-20T12:30:00', 'Reserva Biológica', 'Excursión educativa', 1),
    (5, '2024-11-21T16:00:00', 'Aula 4B', 'Taller sobre reciclaje', 1),
    (3, '2024-11-22T10:00:00', 'Galería de Arte', 'Exposición de arte local', 1),
    (1, '2024-11-24T09:00:00', 'Estadio Municipal', 'Clásico de fútbol escolar', 1),
    (4, '2024-11-25T15:30:00', 'Patio Principal', 'Carrera de obstáculos', 1),
    (5, '2024-11-26T08:00:00', 'Aula de Ciencias', 'Clase formativa de ciencias naturales', 1),
    (1, '2024-11-28T11:30:00', 'Polideportivo', 'Campeonato de voleibol', 1),
    (3, '2024-11-30T13:00:00', 'Biblioteca', 'Club de lectura cultural', 1),

    -- Diciembre
    (2, '2024-12-01T14:00:00', 'Bosque del Niño', 'Excursión a la montaña', 1),
    (4, '2024-12-02T10:00:00', 'Parque de Diversiones', 'Día recreativo', 1),
    (5, '2024-12-03T09:00:00', 'Auditorio', 'Taller de habilidades sociales', 1),
    (1, '2024-12-05T12:00:00', 'Campo Deportivo', 'Entrenamiento de atletismo', 1),
    (3, '2024-12-06T10:30:00', 'Centro Cultural', 'Proyección de película cultural', 1),
    (4, '2024-12-08T14:30:00', 'Sala de Arte', 'Pintura al aire libre', 1),
    (2, '2024-12-10T09:00:00', 'Jardín Botánico', 'Excursión botánica', 1),
    (5, '2024-12-12T13:00:00', 'Laboratorio', 'Clase formativa de química', 1),
    (4, '2024-12-14T15:00:00', 'Parque Infantil', 'Actividad recreativa para niños', 1),
    (1, '2024-12-15T08:00:00', 'Gimnasio', 'Competencia de salto alto', 1),
    (1, '2024-12-18T09:00:00', 'Cancha Principal', 'Partido de fútbol amistoso', 1),
    (2, '2024-12-18T14:00:00', 'Plaza de la Cultura', 'Excursión a la Plaza de la Cultura', 1),
    (3, '2024-12-19T10:30:00', 'Museo de los Niños', 'Actividad cultural educativa', 1),
    (4, '2024-12-20T11:00:00', 'Parque Recreativo', 'Juegos recreativos al aire libre', 1),
    (5, '2024-12-21T13:00:00', 'Auditorio Principal', 'Charla formativa sobre medio ambiente', 1),
    (4, '2024-12-22T15:00:00', 'Patio Central', 'Competencia de juegos recreativos', 1),
    (1, '2024-12-23T08:00:00', 'Piscina Municipal', 'Competencia de natación', 1),
    (3, '2024-12-24T09:30:00', 'Centro Cultural', 'Presentación de talentos culturales', 1),
    (2, '2024-12-26T14:00:00', 'Reserva Natural', 'Excursión ecológica en la reserva', 1),
    (5, '2024-12-27T10:00:00', 'Sala de Ciencias', 'Taller formativo sobre reciclaje', 1);
GO

-- Insertar 15 registros en la tabla pagos
INSERT INTO pagos (id_nino, id_padre, id_tipo_pago, fecha_pago, monto, metodo_pago, referencia_factura, fecha_creacion, ultima_actualizacion)
VALUES
    (1, 1, 1, '2024-11-01', 1000.00, 'Efectivo', 'FAC-001', GETDATE(), GETDATE()),
    (2, 2, 2, '2024-11-02', 20000.00, 'Tarjeta de Crédito', 'FAC-002', GETDATE(), GETDATE()),
    (3, 3, 3, '2024-11-05', 1500.00, 'Transferencia', 'FAC-003', GETDATE(), GETDATE()),
    (4, 4, 4, '2024-11-07', 2500.00, 'Efectivo', 'FAC-004', GETDATE(), GETDATE()),
    (5, 5, 1, '2024-11-10', 30000.00, 'Tarjeta de Débito', 'FAC-005', GETDATE(), GETDATE()),
    (6, 6, 2, '2024-11-12', 1800.00, 'Transferencia', 'FAC-006', GETDATE(), GETDATE()),
    (7, 7, 3, '2024-11-15', 22000.00, 'Efectivo', 'FAC-007', GETDATE(), GETDATE()),
    (1, 1, 4, '2024-11-18', 5000.00, 'Cheque', 'FAC-008', GETDATE(), GETDATE()),
    (2, 2, 1, '2024-11-20', 1000.00, 'Tarjeta de Crédito', 'FAC-009', GETDATE(), GETDATE()),
    (3, 3, 2, '2024-11-22', 25000.00, 'Transferencia', 'FAC-010', GETDATE(), GETDATE()),
    (4, 4, 3, '2024-11-24', 4000.00, 'Efectivo', 'FAC-011', GETDATE(), GETDATE()),
    (5, 5, 4, '2024-11-26', 3500.00, 'Tarjeta de Débito', 'FAC-012', GETDATE(), GETDATE()),
    (6, 6, 1, '2024-11-28', 1200.00, 'Transferencia', 'FAC-013', GETDATE(), GETDATE()),
    (7, 7, 2, '2024-11-29', 2705.00, 'Efectivo', 'FAC-014', GETDATE(), GETDATE()),
    (1, 1, 3, '2024-11-30', 3010.00, 'Tarjeta de Crédito', 'FAC-015', GETDATE(), GETDATE());
GO

-- Insertar 10 registros en la tabla docentes
INSERT INTO docentes (id_usuario, fecha_nacimiento, grupo_asignado, fecha_creacion, ultima_actualizacion, activo)
VALUES
    (1, '1985-02-10', 'Grupo A', GETDATE(), GETDATE(), 1),
    (2, '1990-05-15', 'Grupo B', GETDATE(), GETDATE(), 1),
    (3, '1983-07-20', 'Grupo C', GETDATE(), GETDATE(), 1),
    (4, '1992-03-25', 'Grupo D', GETDATE(), GETDATE(), 1),
    (5, '1987-11-30', 'Grupo E', GETDATE(), GETDATE(), 1),
    (6, '1991-09-10', 'Grupo F', GETDATE(), GETDATE(), 1),
    (7, '1984-01-05', 'Grupo G', GETDATE(), GETDATE(), 1),
    (8, '1993-12-15', 'Grupo H', GETDATE(), GETDATE(), 1),
    (1, '1986-04-18', 'Grupo I', GETDATE(), GETDATE(), 1),
    (2, '1994-08-22', 'Grupo J', GETDATE(), GETDATE(), 1);
GO

-- Insertar 15 registros en la tabla asistencia
INSERT INTO asistencia (id_nino, fecha, presente)
VALUES
    (1, '2024-11-01', 1),
    (2, '2024-11-01', 1),
    (3, '2024-11-01', 0),
    (4, '2024-11-02', 1),
    (5, '2024-11-02', 1),
    (6, '2024-11-02', 0),
    (7, '2024-11-03', 1),
    (1, '2024-11-03', 1),
    (2, '2024-11-04', 1),
    (3, '2024-11-04', 1),
    (4, '2024-11-05', 0),
    (5, '2024-11-05', 1),
    (6, '2024-11-06', 1),
    (7, '2024-11-06', 0),
    (1, '2024-11-06', 1);
GO

-- Insertar 15 registros en la tabla tareas
INSERT INTO tareas (id_profesor, nombre, descripcion, calificacion, fecha_asignada, fecha_entrega, activo)
VALUES
    (1, 'Colorear dibujos', 'Colorear un dibujo de animales.', 90, '2024-11-01', '2024-11-05', 1),
    (2, 'Aprender las vocales', 'Identificar y escribir las vocales en una hoja.', 85, '2024-11-02', '2024-11-06', 1),
    (3, 'Cantar una canción', 'Aprender y cantar la canción "Estrellita, ¿dónde estás?".', 95, '2024-11-03', '2024-11-07', 1),
    (4, 'Dibujar una casa', 'Dibujar y colorear una casa con ventanas y puertas.', 88, '2024-11-04', '2024-11-08', 1),
    (5, 'Contar hasta 10', 'Practicar contar hasta 10 con los dedos.', 92, '2024-11-05', '2024-11-09', 1),
    (6, 'Aprender los colores', 'Identificar y nombrar los colores básicos: rojo, azul, amarillo.', 80, '2024-11-06', '2024-11-10', 1),
    (7, 'Recortar figuras', 'Recortar figuras geométricas de una hoja de papel.', 89, '2024-11-07', '2024-11-11', 1),
    (1, 'Pegar figuras', 'Pegar recortes de figuras geométricas en una hoja.', 93, '2024-11-08', '2024-11-12', 1),
    (2, 'Dibujar su familia', 'Dibujar y colorear a los miembros de su familia.', 87, '2024-11-09', '2024-11-13', 1),
    (3, 'Contar cuentos', 'Llevar un cuento corto y leerlo con sus padres.', 90, '2024-11-10', '2024-11-14', 1),
    (4, 'Hacer un collage', 'Crear un collage con recortes de revistas.', 94, '2024-11-11', '2024-11-15', 1),
    (5, 'Practicar figuras', 'Identificar círculos, cuadrados y triángulos en objetos de casa.', 91, '2024-11-12', '2024-11-16', 1),
    (6, 'Juego de memoria', 'Jugar con tarjetas de memoria para encontrar pares iguales.', 86, '2024-11-13', '2024-11-17', 1),
    (7, 'Cuidar una planta', 'Regar una planta pequeña y observar su crecimiento.', 84, '2024-11-14', '2024-11-18', 1),
    (1, 'Crear una máscara', 'Hacer una máscara de animales con cartón y decorarla.', 97, '2024-11-15', '2024-11-19', 1);
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
