-------------------------------------------- INSERTS --------------------------------------------
INSERT INTO roles (nombre)
VALUES ('Administrador'),
       ('Docente'),
       ('Padre');
GO

INSERT INTO tipo_pagos (nombre_tipo_pago)
VALUES ('Matr�cula'),
       ('Mensualidad'),
       ('Excursi�n'),
       ('Otro');
Go

INSERT INTO tipo_actividad (nombre_tipo_actividad)
VALUES ('Deportes'),
       ('Excursi�n'),
       ('Actividad Cultural');
GO

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
GO

INSERT INTO condiciones_medicas (nombre_condicion)
VALUES ('Asma'),
       ('Diabetes tipo 1'),
       ('Epilepsia'),
       ('D�ficit de atenci�n e hiperactividad (TDAH)'),
       ('Alergias alimentarias'),
       ('Dermatitis at�pica'),
       ('Anemia'),
       ('Infecciones respiratorias recurrentes');
GO

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

-------------------------------------------- INSERTS TABLA DE USUARIOS --------------------------------------------

-- No es necesario hacer estos inserts de aca para abajo, ya que ocupamos primero usuarios con contrasenas hasheadas para luego hacer relaciones con los ninos.

-- Insertar usuario administrador
EXEC GestionarUsuario
    @Cedula = '1234567890',
    @Nombre = 'Juan Perez',
    @Contrasena = 'Contrase�aSegura123',
    @Num_Telefono = 123456789,
    @Direccion = 'Calle Falsa 123',
    @CorreoElectronico = 'juan.perez@example.com',
    @IdRol = 1; -- Administrador
Go

-- Insertar usuario docente
EXEC GestionarUsuario
    @Cedula = '1234567810',
    @Nombre = 'Daniel Hernandez',
    @Contrasena = 'Contrase�aSegura123',
    @Num_Telefono = 123456789,
    @Direccion = 'Calle Falsa 456',
    @CorreoElectronico = 'Daniel.Hernandez@example.com',
    @IdRol = 2; -- Administrador
Go

EXEC GestionarUsuario 
    @Cedula = '123456789', 
    @Nombre = 'Sof�a Mart�nez', 
    @Contrasena = 'ContrasenaSegura123',
    @Num_Telefono = 987654321, 
    @Direccion = 'Calle de la Educaci�n 789', 
    @CorreoElectronico = 'sofia.martinez@kinder.com', 
    @IdRol = 2; -- Docente
GO

-- Insertar usuario padre
EXEC GestionarUsuario
    @Cedula = '0987654321',
    @Nombre = 'Mar�a Gonz�lez',
    @Contrasena = 'Contrase�aPadre123',
    @Num_Telefono = 987654321,
    @Direccion = 'Avenida Siempre Viva 456',
    @CorreoElectronico = 'maria.gonzalez@example.com',
    @IdRol = 3; -- Padres
GO

-- Insertar usuario nino
EXEC GestionarNino 
    @Cedula = '12345678', 
    @NombreNino = 'Juan P�rez', 
    @FechaNacimiento = '2018-05-20', 
    @Direccion = 'Calle Falsa 123', 
    @Poliza = 'P�liza 1';
GO

-- Actualizar el perfil del docente
EXEC GestionarDocente 
    @IdDocente = 5,                -- ID del docente a modificar
    @IdUsuario = 4,               -- ID de usuario
    @FechaNacimiento = '1990-05-15', -- Nueva fecha de nacimiento
    @GrupoAsignado = 'Grupo A';     -- Nuevo grupo asignado
GO

-------------------------------------------- INSERTS TABLA DE PAGOS --------------------------------------------
-- Registrar pagos
EXEC RegistrarPago 
	@IdNino = 1, 
	@IdPadre = 3, 
	@IdTipoPago = 1, 
	@FechaPago = '2024-01-26', 
	@Monto = 500.00,
	@MetodoPago = 'Tarjeta', 
	@ReferenciaFactura = '12345678';
GO

-------------------------------------------- INSERTS TABLA DE EVALUACIONES --------------------------------------------
-- Registrar evaluaciones de los ni�os
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (1, 'Educacion Fisica', 97, '2024-09-13', 'Buen desempe�o en la evaluaci�n de educaci�n f�sica.');
GO

-------------------------------------------- INSERTS TABLA DE CONTACTOS DE EMERGENCIA --------------------------------------------
-- Registrar contactos de emergencia
EXEC GestionarContactoEmergencia @NombreContacto = 'Carlos Martinez', @Relacion = 'Padre', @Telefono = 555123456,
     @Direccion = 'Calle Falsa 123', @IdNino = 1;
GO

-------------------------------------------- INSERTS TABLA DE RELACIONES --------------------------------------------
-- Relaci�n padres con sus ni�os
INSERT INTO rel_padres_ninos 
	(id_padre, id_nino, relacion)
VALUES 
	(3, 1, 'Padre')
GO

-- Registrar asistencia de los ni�os
EXEC RegistrarAsistencia 
	@IdNino = 1,
	@Fecha = '2024-09-15', 
	@HoraEntrada = '08:00',
	@HoraSalida = '14:00',
	@Estado = 'Presente';
GO

-- Registrar actividades
EXEC RegistrarActividad 
	@IdTipoActividad = 1, 
	@Fecha = '2024-09-17',
	@Lugar = 'Cancha de Deportes', 
	@IdNino = 1,
     @Asistencia = 'Ausente';
GO

-- Insertar observaciones de los docentes
INSERT INTO observaciones_docentes 
	(id_nino, id_docente, tipo, descripcion, fecha)
VALUES 
	(1, 5, 'Asistencia', 'No entreg� los trabajos asignados a tiempo.', '2024-09-20');
GO

-- Registrar progreso acad�mico
INSERT INTO progreso_academico 
	(id_nino, area_academica, nivel_progreso, descripcion)
VALUES 
	(1, 'Ortograf�a', 'Inicial', 'Reconoce algunas letras del abecedario y empieza a identificarlas en palabras simples.');
GO

-- Relaci�n entre docentes, ni�os y materias
INSERT INTO rel_docente_nino_materia 
	(id_docente, id_nino, materia)
VALUES 
	(5, 1, 'Educacion Fisica');
GO

-- Asignar alergias 
INSERT INTO rel_nino_alergia (id_nino, id_alergia)
VALUES (1, 1);
GO

-- Asignar condiciones m�dicas 
INSERT INTO rel_nino_condicion (id_nino, id_condicion)
VALUES (1, 1);
Go

-- Asignar medicamentos 
INSERT INTO rel_nino_medicamento (id_nino, id_medicamento)
VALUES (1, 4);
GO