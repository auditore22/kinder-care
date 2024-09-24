-- Insertar usuarios
EXEC GestionarUsuario @Nombre = 'Carlos Martinez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Laura Gomez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Pedro Ramirez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Ana Sanchez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Ricardo Torres', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Claudia Lopez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Pablo Vega', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Angela Ruiz', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Antonio Fernandez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Marta Morales', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Roberto Gonzalez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Isabel Suarez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Oscar Jimenez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Carmen Torres', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Fernando Mendez', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Patricia Quintana', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Rafael Ortiz', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Luisa Navarro', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Mario Diaz', @ContrasenaHash = 'padre123', @IdRol = 3;
EXEC GestionarUsuario @Nombre = 'Elena Ramirez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Julio Perez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Sofia Lopez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Teresa Hernandez', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Esteban Vargas', @ContrasenaHash = 'padre123', @IdRol = 3;

EXEC GestionarUsuario @Nombre = 'Juan Perez', @ContrasenaHash = 'admin123', @IdRol = 1;
EXEC GestionarUsuario @Nombre = 'Pedro Ramirez', @ContrasenaHash = 'admin456', @IdRol = 1;
EXEC GestionarUsuario @Nombre = 'Laura Gomez', @ContrasenaHash = 'docente123', @IdRol = 2;
EXEC GestionarUsuario @Nombre = 'Sofia Perez', @ContrasenaHash = 'docente456', @IdRol = 2;
GO


-- Insertar docentes
EXEC GestionarDocente @IdUsuario = 2, @Nombre = 'Laura', @Apellido = 'Gomez', @FechaNacimiento = '1985-05-12',
     @Direccion = 'Av. Siempre Viva 123', @Telefono = '555123456', @CorreoElectronico = 'lgomez@example.com',
     @GrupoAsignado = 'Preescolar';
GO
EXEC GestionarDocente @IdUsuario = 6, @Nombre = 'Sofia', @Apellido = 'Perez', @FechaNacimiento = '1990-07-15',
     @Direccion = 'Calle Secundaria 123', @Telefono = '555987654', @CorreoElectronico = 'sperez@example.com',
     @GrupoAsignado = 'Preescolar';
GO


-- Insertar padres
EXEC GestionarPadre @IdUsuario = 1;
EXEC GestionarPadre @IdUsuario = 2;
EXEC GestionarPadre @IdUsuario = 3;
EXEC GestionarPadre @IdUsuario = 4;
EXEC GestionarPadre @IdUsuario = 5;
EXEC GestionarPadre @IdUsuario = 6;
EXEC GestionarPadre @IdUsuario = 7;
EXEC GestionarPadre @IdUsuario = 8;
EXEC GestionarPadre @IdUsuario = 9;
EXEC GestionarPadre @IdUsuario = 10;
EXEC GestionarPadre @IdUsuario = 11;
EXEC GestionarPadre @IdUsuario = 12;
EXEC GestionarPadre @IdUsuario = 13;
EXEC GestionarPadre @IdUsuario = 14;
EXEC GestionarPadre @IdUsuario = 15;
EXEC GestionarPadre @IdUsuario = 16;
EXEC GestionarPadre @IdUsuario = 17;
EXEC GestionarPadre @IdUsuario = 18;
EXEC GestionarPadre @IdUsuario = 19;
EXEC GestionarPadre @IdUsuario = 20;
EXEC GestionarPadre @IdUsuario = 21;
EXEC GestionarPadre @IdUsuario = 22;
EXEC GestionarPadre @IdUsuario = 23;
EXEC GestionarPadre @IdUsuario = 24;
GO


-- Insertar niños
EXEC GestionarNino @NombreNino = 'Lucas Martinezz', @FechaNacimiento = '2015-08-20', @Direccion = 'Calle Falsa 123',
     @Poliza = 'PM123';
EXEC GestionarNino @NombreNino = 'Valentina Ramirez', @FechaNacimiento = '2016-03-15', @Direccion = 'Calle Verde 456',
     @Poliza = 'MS456';
EXEC GestionarNino @NombreNino = 'Mateo Torres', @FechaNacimiento = '2015-07-18', @Direccion = 'Calle Verde 456',
     @Poliza = 'VG456';
EXEC GestionarNino @NombreNino = 'Camila Vega', @FechaNacimiento = '2016-02-11', @Direccion = 'Calle Roja 123',
     @Poliza = 'JT123';
EXEC GestionarNino @NombreNino = 'Diego Fernandez', @FechaNacimiento = '2017-05-23', @Direccion = 'Av. del Sol 987',
     @Poliza = 'MV987';
EXEC GestionarNino @NombreNino = 'Sofia Gonzalez', @FechaNacimiento = '2014-03-12', @Direccion = 'Av. del Norte 654',
     @Poliza = 'DL654';
EXEC GestionarNino @NombreNino = 'Javier Jimenez', @FechaNacimiento = '2015-09-30', @Direccion = 'Calle Amarilla 111',
     @Poliza = 'SP111';
EXEC GestionarNino @NombreNino = 'Lucia Mendez', @FechaNacimiento = '2016-04-01', @Direccion = 'Av. Central 500',
     @Poliza = 'AH500';
EXEC GestionarNino @NombreNino = 'Daniel Ortiz', @FechaNacimiento = '2017-09-15', @Direccion = 'Av. Central 500',
     @Poliza = 'CH501';
EXEC GestionarNino @NombreNino = 'Isabel Diaz', @FechaNacimiento = '2014-12-10', @Direccion = 'Av. Azul 789',
     @Poliza = 'LF789';
EXEC GestionarNino @NombreNino = 'Rodrigo Diaz', @FechaNacimiento = '2014-10-22', @Direccion = 'Av. Azul 789',
     @Poliza = 'SF789';
EXEC GestionarNino @NombreNino = 'Emilio Perez', @FechaNacimiento = '2015-10-10', @Direccion = 'Calle Estrella 99',
     @Poliza = 'LM500';
EXEC GestionarNino @NombreNino = 'Paula Lopez', @FechaNacimiento = '2016-11-20', @Direccion = 'Calle Estrella 99',
     @Poliza = 'MM501';
EXEC GestionarNino @NombreNino = 'Miguel Hernandez', @FechaNacimiento = '2014-08-08', @Direccion = 'Calle Norte 12',
     @Poliza = 'IJ012';
EXEC GestionarNino @NombreNino = 'Leonardo Vargas', @FechaNacimiento = '2016-12-24', @Direccion = 'Av. Sur 99',
     @Poliza = 'RD099';
GO

-- Relación padres con sus niños
INSERT INTO rel_padres_ninos (id_padre, id_nino, relacion)
VALUES (1, 1, 'Padre'),
       (2, 1, 'Madre'),

       (3, 2, 'Padre'),
       (4, 2, 'Madre'),

       (5, 3, 'Padre'),
       (6, 3, 'Madre'),

       (7, 4, 'Padre'),
       (8, 4, 'Madre'),

       (9, 5, 'Padre'),
       (10, 5, 'Madre'),

       (11, 6, 'Padre'),
       (12, 6, 'Madre'),

       (13, 7, 'Padre'),
       (14, 7, 'Madre'),

       (15, 8, 'Padre'),
       (16, 8, 'Madre'),

       (17, 9, 'Padre'),
       (18, 9, 'Madre'),

       (19, 10, 'Padre'),
       (20, 10, 'Madre'),
       (19, 11, 'Padre'),
       (20, 11, 'Madre'),

       (21, 12, 'Padre'),

       (22, 13, 'Madre'),

       (23, 14, 'Abuela'),

       (24, 15, 'Padre')
GO

-- Registrar asistencia de los niños
EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-15', @HoraEntrada = '08:00', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-15', @HoraEntrada = '08:15', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-15', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';

EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-16', @HoraEntrada = '08:01', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-16', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-16', @HoraEntrada = '07:53', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 4, @Fecha = '2024-09-16', @HoraEntrada = '08:05', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 5, @Fecha = '2024-09-16', @HoraEntrada = '08:07', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 6, @Fecha = '2024-09-16', @HoraEntrada = '07:57', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 7, @Fecha = '2024-09-16', @HoraEntrada = '07:55', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 8, @Fecha = '2024-09-16', @HoraEntrada = '08:03', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 9, @Fecha = '2024-09-16', @HoraEntrada = '08:18', @HoraSalida = '13:55',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 10, @Fecha = '2024-09-16', @HoraEntrada = '07:50', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 11, @Fecha = '2024-09-16', @HoraEntrada = '08:11', @HoraSalida = '14:00',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 12, @Fecha = '2024-09-16', @HoraEntrada = '08:16', @HoraSalida = '14:00',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 13, @Fecha = '2024-09-16', @HoraEntrada = '08:04', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 14, @Fecha = '2024-09-16', @HoraEntrada = '07:59', @HoraSalida = '13:57',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 15, @Fecha = '2024-09-16', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';

EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-17', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-17', @HoraEntrada = '07:57', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-17', @HoraEntrada = '08:05', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 4, @Fecha = '2024-09-17', @HoraEntrada = '07:50', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 5, @Fecha = '2024-09-17', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 6, @Fecha = '2024-09-17', @HoraEntrada = '08:09', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 7, @Fecha = '2024-09-17', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 8, @Fecha = '2024-09-17', @HoraEntrada = '07:59', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 9, @Fecha = '2024-09-17', @HoraEntrada = '07:58', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 10, @Fecha = '2024-09-17', @HoraEntrada = '08:04', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 11, @Fecha = '2024-09-17', @HoraEntrada = '08:13', @HoraSalida = '13:55',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 12, @Fecha = '2024-09-17', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 13, @Fecha = '2024-09-17', @HoraEntrada = '08:25', @HoraSalida = '13:50',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 14, @Fecha = '2024-09-17', @HoraEntrada = '08:08', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 15, @Fecha = '2024-09-17', @HoraEntrada = '08:01', @HoraSalida = '14:00',
     @Estado = 'Presente';

EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-18', @HoraEntrada = '08:01', @HoraSalida = '13:50',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-18', @HoraEntrada = '08:21', @HoraSalida = '13:55',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-18', @HoraEntrada = '08:03', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 4, @Fecha = '2024-09-18', @HoraEntrada = '07:56', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 5, @Fecha = '2024-09-18', @HoraEntrada = '08:13', @HoraSalida = '14:00',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 6, @Fecha = '2024-09-18', @HoraEntrada = '08:02', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 7, @Fecha = '2024-09-18', @HoraEntrada = '07:57', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 8, @Fecha = '2024-09-18', @HoraEntrada = '08:00', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 9, @Fecha = '2024-09-18', @HoraEntrada = '08:17', @HoraSalida = '14:00',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 10, @Fecha = '2024-09-18', @HoraEntrada = '07:55', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 11, @Fecha = '2024-09-18', @HoraEntrada = '08:06', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 12, @Fecha = '2024-09-18', @HoraEntrada = '08:05', @HoraSalida = '13:50',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 13, @Fecha = '2024-09-18', @HoraEntrada = '08:00', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 14, @Fecha = '2024-09-18', @HoraEntrada = '08:01', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 15, @Fecha = '2024-09-18', @HoraEntrada = '08:18', @HoraSalida = '13:50',
     @Estado = 'Tarde';

EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-19', @HoraEntrada = '08:01', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-19', @HoraEntrada = '07:55', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-19', @HoraEntrada = '08:09', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 4, @Fecha = '2024-09-19', @HoraEntrada = '08:00', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 5, @Fecha = '2024-09-19', @HoraEntrada = '08:00', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 6, @Fecha = '2024-09-19', @HoraEntrada = '08:01', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 7, @Fecha = '2024-09-19', @HoraEntrada = '08:06', @HoraSalida = '13:50',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 8, @Fecha = '2024-09-19', @HoraEntrada = '08:07', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 9, @Fecha = '2024-09-19', @HoraEntrada = '07:51', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 10, @Fecha = '2024-09-19', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 11, @Fecha = '2024-09-19', @HoraEntrada = '08:06', @HoraSalida = '14:00',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 12, @Fecha = '2024-09-19', @HoraEntrada = '08:13', @HoraSalida = '14:00',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 13, @Fecha = '2024-09-19', @HoraEntrada = '07:57', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 14, @Fecha = '2024-09-19', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 15, @Fecha = '2024-09-19', @HoraEntrada = '08:06', @HoraSalida = '13:55',
     @Estado = 'Presente';

EXEC RegistrarAsistencia @IdNino = 1, @Fecha = '2024-09-20', @HoraEntrada = '08:04', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 2, @Fecha = '2024-09-20', @HoraEntrada = '08:15', @HoraSalida = '13:50',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 3, @Fecha = '2024-09-20', @HoraEntrada = '08:06', @HoraSalida = '14:05',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 4, @Fecha = '2024-09-20', @HoraEntrada = '08:00', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 5, @Fecha = '2024-09-20', @HoraEntrada = '08:18', @HoraSalida = '14:10',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 6, @Fecha = '2024-09-20', @HoraEntrada = '07:56', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 7, @Fecha = '2024-09-20', @HoraEntrada = '08:00', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 8, @Fecha = '2024-09-20', @HoraEntrada = '08:01', @HoraSalida = '13:55',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 9, @Fecha = '2024-09-20', @HoraEntrada = '08:16', @HoraSalida = '13:50',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 10, @Fecha = '2024-09-20', @HoraEntrada = '08:11', @HoraSalida = '13:50',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 11, @Fecha = '2024-09-20', @HoraEntrada = NULL, @HoraSalida = NULL,
     @Estado = 'Ausente';
EXEC RegistrarAsistencia @IdNino = 12, @Fecha = '2024-09-20', @HoraEntrada = '07:55', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 13, @Fecha = '2024-09-20', @HoraEntrada = '08:30', @HoraSalida = '13:55',
     @Estado = 'Tarde';
EXEC RegistrarAsistencia @IdNino = 14, @Fecha = '2024-09-20', @HoraEntrada = '08:04', @HoraSalida = '14:10',
     @Estado = 'Presente';
EXEC RegistrarAsistencia @IdNino = 15, @Fecha = '2024-09-20', @HoraEntrada = '08:12', @HoraSalida = '14:00',
     @Estado = 'Tarde';
GO


-- Registrar pagos
EXEC RegistrarPago @IdNino = 1, @IdPadre = 1, @IdTipoPago = 1, @FechaPago = '2024-01-26', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345678';
EXEC RegistrarPago @IdNino = 1, @IdPadre = 1, @IdTipoPago = 2, @FechaPago = '2024-09-03', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345679';

EXEC RegistrarPago @IdNino = 2, @IdPadre = 3, @IdTipoPago = 1, @FechaPago = '2024-01-30', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345680';
EXEC RegistrarPago @IdNino = 2, @IdPadre = 3, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345681';

EXEC RegistrarPago @IdNino = 3, @IdPadre = 5, @IdTipoPago = 1, @FechaPago = '2024-01-30', @Monto = 500.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345682';
EXEC RegistrarPago @IdNino = 3, @IdPadre = 6, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345684';

EXEC RegistrarPago @IdNino = 4, @IdPadre = 8, @IdTipoPago = 1, @FechaPago = '2024-01-31', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345685';
EXEC RegistrarPago @IdNino = 4, @IdPadre = 8, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345686';

EXEC RegistrarPago @IdNino = 5, @IdPadre = 9, @IdTipoPago = 1, @FechaPago = '2024-02-01', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345687';
EXEC RegistrarPago @IdNino = 5, @IdPadre = 9, @IdTipoPago = 2, @FechaPago = '2024-09-05', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345688';

EXEC RegistrarPago @IdNino = 6, @IdPadre = 11, @IdTipoPago = 1, @FechaPago = '2024-01-30', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345689';
EXEC RegistrarPago @IdNino = 6, @IdPadre = 11, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345690';

EXEC RegistrarPago @IdNino = 7, @IdPadre = 13, @IdTipoPago = 1, @FechaPago = '2024-01-31', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345691';
EXEC RegistrarPago @IdNino = 7, @IdPadre = 13, @IdTipoPago = 2, @FechaPago = '2024-09-03', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345692';

EXEC RegistrarPago @IdNino = 8, @IdPadre = 15, @IdTipoPago = 1, @FechaPago = '2024-02-01', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345693';
EXEC RegistrarPago @IdNino = 8, @IdPadre = 15, @IdTipoPago = 2, @FechaPago = '2024-09-03', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345694';

EXEC RegistrarPago @IdNino = 9, @IdPadre = 18, @IdTipoPago = 1, @FechaPago = '2024-01-29', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345695';
EXEC RegistrarPago @IdNino = 9, @IdPadre = 17, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345696';

EXEC RegistrarPago @IdNino = 10, @IdPadre = 19, @IdTipoPago = 1, @FechaPago = '2024-01-31', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345697';
EXEC RegistrarPago @IdNino = 10, @IdPadre = 19, @IdTipoPago = 2, @FechaPago = '2024-09-06', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345698';

EXEC RegistrarPago @IdNino = 11, @IdPadre = 19, @IdTipoPago = 1, @FechaPago = '2024-01-31', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345699';
EXEC RegistrarPago @IdNino = 11, @IdPadre = 19, @IdTipoPago = 2, @FechaPago = '2024-09-06', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345700';

EXEC RegistrarPago @IdNino = 12, @IdPadre = 21, @IdTipoPago = 1, @FechaPago = '2024-01-30', @Monto = 500.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345701';
EXEC RegistrarPago @IdNino = 12, @IdPadre = 21, @IdTipoPago = 2, @FechaPago = '2024-09-04', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345702';

EXEC RegistrarPago @IdNino = 13, @IdPadre = 22, @IdTipoPago = 1, @FechaPago = '2024-01-30', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345703';
EXEC RegistrarPago @IdNino = 13, @IdPadre = 22, @IdTipoPago = 2, @FechaPago = '2024-09-03', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345704';

EXEC RegistrarPago @IdNino = 14, @IdPadre = 23, @IdTipoPago = 1, @FechaPago = '2024-01-29', @Monto = 500.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345705';
EXEC RegistrarPago @IdNino = 14, @IdPadre = 23, @IdTipoPago = 2, @FechaPago = '2024-09-02', @Monto = 200.00,
     @MetodoPago = 'Efectivo', @ReferenciaFactura = '12345706';

EXEC RegistrarPago @IdNino = 15, @IdPadre = 24, @IdTipoPago = 1, @FechaPago = '2024-01-26', @Monto = 500.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345707';
EXEC RegistrarPago @IdNino = 15, @IdPadre = 24, @IdTipoPago = 2, @FechaPago = '2024-09-04', @Monto = 200.00,
     @MetodoPago = 'Tarjeta', @ReferenciaFactura = '12345708';

GO



-- Registrar actividades
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 1,
     @Asistencia = 'Ausente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 2,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 3,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 4,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 5,
     @Asistencia = 'Ausente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 6,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 7,
     @Asistencia = 'Ausente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 8,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 9,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 10,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 11,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 12,
     @Asistencia = 'Ausente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 13,
     @Asistencia = 'Tarde';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 14,
     @Asistencia = 'Presente';
EXEC RegistrarActividad @IdTipoActividad = 1, @Fecha = '2024-09-17', @Lugar = 'Cancha de Deportes', @IdNino = 15,
     @Asistencia = 'Presente';
GO


-- Registrar evaluaciones de los niños
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (1, 'Educacion Fisica', 97, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (2, 'Educacion Fisica', 94, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (3, 'Educacion Fisica', 100, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (4, 'Educacion Fisica', 90, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (5, 'Educacion Fisica', 93, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (6, 'Educacion Fisica', 98, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (7, 'Educacion Fisica', 91, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (8, 'Educacion Fisica', 92, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (9, 'Educacion Fisica', 90, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (10, 'Educacion Fisica', 100, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (11, 'Educacion Fisica', 90, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (12, 'Educacion Fisica', 99, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (13, 'Educacion Fisica', 93, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (14, 'Educacion Fisica', 96, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (15, 'Educacion Fisica', 98, '2024-09-13', 'Buen desempeño en la evaluación de educación física.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (1, 'Ortografia', 89, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (2, 'Ortografia', 90, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (3, 'Ortografia', 91, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (4, 'Ortografia', 84, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (5, 'Ortografia', 93, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (6, 'Ortografia', 93, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (7, 'Ortografia', 77, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (8, 'Ortografia', 77, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (9, 'Ortografia', 88, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (10, 'Ortografia', 98, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (11, 'Ortografia', 99, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (12, 'Ortografia', 82, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (13, 'Ortografia', 84, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (14, 'Ortografia', 87, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');
INSERT INTO evaluaciones (id_nino, asignatura, puntaje, fecha, comentarios)
VALUES (15, 'Ortografia', 90, '2024-09-17', 'Evaluación de ortografía con resultados positivos.');


-- Insertar observaciones de los docentes
INSERT INTO observaciones_docentes (id_nino, id_docente, tipo, descripcion, fecha)
VALUES (11, 2, 'Asistencia', 'No entregó los trabajos asignados a tiempo.', '2024-09-20'),
       (2, 2, 'Participación', 'El estudiante mostró un comportamiento inapropiado en clase.', '2024-09-20'),
       (14, 1, 'Asistencia', 'Falta recurrente a las clases sin justificación.', '2024-09-20'),
       (3, 1, 'Rendimiento', 'No entregó los trabajos asignados a tiempo.', '2024-09-20'),
       (9, 1, 'Asistencia', 'No entregó los trabajos asignados a tiempo.', '2024-09-20');
GO


-- Registrar progreso académico
INSERT INTO progreso_academico (id_nino, area_academica, nivel_progreso, descripcion)
VALUES (1, 'Ortografía', 'Inicial',
        'Reconoce algunas letras del abecedario y empieza a identificarlas en palabras simples.'),
       (2, 'Ortografía', 'Básico',
        'Puede asociar algunas letras con sus sonidos, comenzando a escribir letras sueltas.'),
       (3, 'Ortografía', 'Básico',
        'Puede asociar algunas letras con sus sonidos, comenzando a escribir letras sueltas.'),
       (4, 'Ortografía', 'Intermedio',
        'Escribe palabras sencillas como su nombre o palabras comunes, aunque con algunos errores.'),
       (5, 'Ortografía', 'Intermedio',
        'Escribe palabras sencillas como su nombre o palabras comunes, aunque con algunos errores.'),
       (6, 'Ortografía', 'Avanzado',
        'Puede escribir frases cortas y copiar oraciones con mayor precisión, pero con errores menores.'),
       (7, 'Ortografía', 'Inicial',
        'Reconoce algunas letras del abecedario y empieza a identificarlas en palabras simples.'),
       (8, 'Ortografía', 'Inicial',
        'Reconoce algunas letras del abecedario y empieza a identificarlas en palabras simples.'),
       (9, 'Ortografía', 'Dominio',
        'Escribe oraciones completas con pocas faltas ortográficas y entiende reglas básicas de ortografía.'),
       (10, 'Ortografía', 'Avanzado',
        'Puede escribir frases cortas y copiar oraciones con mayor precisión, pero con errores menores.'),
       (11, 'Ortografía', 'Avanzado',
        'Puede escribir frases cortas y copiar o con mayor precisión, pero con errores menores.'),
       (12, 'Ortografía', 'Básico',
        'Puede asociar algunas letras con sus sonidos, comenzando a escribir letras sueltas.'),
       (13, 'Ortografía', 'Básico',
        'Puede asociar algunas letras con sus sonidos, comenzando a escribir letras sueltas.'),
       (14, 'Ortografía', 'Avanzado',
        'Puede escribir frases cortas y copiar oraciones con mayor precisión, pero con errores menores.   '),
       (15, 'Ortografía', 'Intermedio',
        'Escribe palabras sencillas como su nombre o palabras comunes, aunque con algunos errores.');
GO


-- Relación entre docentes, niños y materias
INSERT INTO rel_docente_nino_materia (id_docente, id_nino, materia)
VALUES (1, 1, 'Educacion Fisica'),
       (1, 2, 'Educacion Fisica'),
       (1, 3, 'Educacion Fisica'),
       (1, 4, 'Educacion Fisica'),
       (1, 5, 'Educacion Fisica'),
       (1, 6, 'Educacion Fisica'),
       (1, 7, 'Educacion Fisica'),
       (1, 8, 'Educacion Fisica'),
       (1, 9, 'Educacion Fisica'),
       (1, 10, 'Educacion Fisica'),
       (1, 11, 'Educacion Fisica'),
       (1, 12, 'Educacion Fisica'),
       (1, 13, 'Educacion Fisica'),
       (1, 14, 'Educacion Fisica'),
       (1, 15, 'Educacion Fisica'),
       (2, 1, 'Ortografía'),
       (2, 2, 'Ortografía'),
       (2, 3, 'Ortografía'),
       (2, 4, 'Ortografía'),
       (2, 5, 'Ortografía'),
       (2, 6, 'Ortografía'),
       (2, 7, 'Ortografía'),
       (2, 8, 'Ortografía'),
       (2, 9, 'Ortografía'),
       (2, 10, 'Ortografía'),
       (2, 11, 'Ortografía'),
       (2, 12, 'Ortografía'),
       (2, 13, 'Ortografía'),
       (2, 14, 'Ortografía'),
       (2, 15, 'Ortografía');
GO



-- Registrar contactos de emergencia
EXEC GestionarContactoEmergencia @NombreContacto = 'Carlos Martinez', @Relacion = 'Padre', @Telefono = '555123456',
     @Direccion = 'Calle Falsa 123', @IdNino = 1;
EXEC GestionarContactoEmergencia @NombreContacto = 'Laura Gomez', @Relacion = 'Madre', @Telefono = '555123457',
     @Direccion = 'Calle Verde 456', @IdNino = 1;

EXEC GestionarContactoEmergencia @NombreContacto = 'Pedro Ramirez', @Relacion = 'Padre', @Telefono = '555123458',
     @Direccion = 'Calle Verde 456', @IdNino = 2;
EXEC GestionarContactoEmergencia @NombreContacto = 'Ana Sanchez', @Relacion = 'Madre', @Telefono = '555123459',
     @Direccion = 'Calle Roja 123', @IdNino = 2;

EXEC GestionarContactoEmergencia @NombreContacto = 'Ricardo Torres', @Relacion = 'Padre', @Telefono = '555123460',
     @Direccion = 'Av. del Sol 987', @IdNino = 3;
EXEC GestionarContactoEmergencia @NombreContacto = 'Claudia Lopez', @Relacion = 'Madre', @Telefono = '555123461',
     @Direccion = 'Av. del Norte 654', @IdNino = 3;

EXEC GestionarContactoEmergencia @NombreContacto = 'Pablo Vega', @Relacion = 'Padre', @Telefono = '555123462',
     @Direccion = 'Calle Amarilla 111', @IdNino = 4;
EXEC GestionarContactoEmergencia @NombreContacto = 'Angela Ruiz', @Relacion = 'Madre', @Telefono = '555123463',
     @Direccion = 'Av. Central 500', @IdNino = 4;

EXEC GestionarContactoEmergencia @NombreContacto = 'Antonio Fernandez', @Relacion = 'Padre', @Telefono = '555123464',
     @Direccion = 'Av. Central 500', @IdNino = 5;
EXEC GestionarContactoEmergencia @NombreContacto = 'Marta Morales', @Relacion = 'Madre', @Telefono = '555123465',
     @Direccion = 'Av. Azul 789', @IdNino = 5;

EXEC GestionarContactoEmergencia @NombreContacto = 'Roberto Gonzalez', @Relacion = 'Madre', @Telefono = '555123466',
     @Direccion = 'Av. Azul 789', @IdNino = 6;
EXEC GestionarContactoEmergencia @NombreContacto = 'Isabel Suarez', @Relacion = 'Madre', @Telefono = '555123467',
     @Direccion = 'Calle Estrella 99', @IdNino = 6;

EXEC GestionarContactoEmergencia @NombreContacto = 'Oscar Jimenez', @Relacion = 'Padre', @Telefono = '555123468',
     @Direccion = 'Calle Estrella 99', @IdNino = 7;
EXEC GestionarContactoEmergencia @NombreContacto = 'Carmen Torres', @Relacion = 'Madre', @Telefono = '555123469',
     @Direccion = 'Calle Norte 12', @IdNino = 7;

EXEC GestionarContactoEmergencia @NombreContacto = 'Fernando Mendez', @Relacion = 'Padre', @Telefono = '555123470',
     @Direccion = 'Av. Sur 99', @IdNino = 8;
EXEC GestionarContactoEmergencia @NombreContacto = 'Patricia Quintana', @Relacion = 'Madre', @Telefono = '555123459',
     @Direccion = 'Calle Roja 123', @IdNino = 8;

EXEC GestionarContactoEmergencia @NombreContacto = 'Rafael Ortiz', @Relacion = 'Padre', @Telefono = '555123460',
     @Direccion = 'Av. del Sol 987', @IdNino = 9;
EXEC GestionarContactoEmergencia @NombreContacto = 'Luisa Navarro', @Relacion = 'Madre', @Telefono = '555123461',
     @Direccion = 'Av. del Norte 654', @IdNino = 9;

EXEC GestionarContactoEmergencia @NombreContacto = 'Mario Diaz', @Relacion = 'Padre', @Telefono = '555123462',
     @Direccion = 'Calle Amarilla 111', @IdNino = 10;
EXEC GestionarContactoEmergencia @NombreContacto = 'Elena Ramirez', @Relacion = 'Madre', @Telefono = '555123463',
     @Direccion = 'Av. Central 500', @IdNino = 10;
EXEC GestionarContactoEmergencia @NombreContacto = 'Mario Diaz', @Relacion = 'Padre', @Telefono = '555123464',
     @Direccion = 'Av. Central 500', @IdNino = 11;
EXEC GestionarContactoEmergencia @NombreContacto = 'Elena Ramirez', @Relacion = 'Madre', @Telefono = '555123465',
     @Direccion = 'Av. Azul 789', @IdNino = 11;

EXEC GestionarContactoEmergencia @NombreContacto = 'Julio Perez', @Relacion = 'Padre', @Telefono = '555123467',
     @Direccion = 'Calle Estrella 99', @IdNino = 12;

EXEC GestionarContactoEmergencia @NombreContacto = 'Sofia Lopez', @Relacion = 'Madre', @Telefono = '555123468',
     @Direccion = 'Calle Estrella 99', @IdNino = 13;

EXEC GestionarContactoEmergencia @NombreContacto = 'Teresa Hernandez', @Relacion = 'Abuela', @Telefono = '555123469',
     @Direccion = 'Calle Norte 12', @IdNino = 14;

EXEC GestionarContactoEmergencia @NombreContacto = 'Esteban Vargas', @Relacion = 'Padre', @Telefono = '555123470',
     @Direccion = 'Av. Sur 99', @IdNino = 15;
GO


-- Asignar alergias 
INSERT INTO rel_nino_alergia (id_nino, id_alergia)
VALUES (1, 1),
       (4, 2),
       (8, 3),
       (12, 5),
       (5, 3),
       (5, 2),
       (6, 3),
       (6, 7),
       (7, 1),
       (7, 2),
       (7, 3);

-- Asignar condiciones médicas 
INSERT INTO rel_nino_condicion (id_nino, id_condicion)
VALUES (2, 1),
       (3, 2),
       (9, 3),
       (11, 4), 
       (12, 5),
       (15, 6);

-- Asignar medicamentos 
INSERT INTO rel_nino_medicamento (id_nino, id_medicamento)
VALUES (2, 4),
       (3, 5),
       (11, 3),
       (12, 7),
       (15, 6),
       (1, 2),
       (5, 1);
