-- <<@_______________________________________________________________ REINICIAR INDEX A "0" _______________________________________________________________@>> --
DBCC CHECKIDENT ('ninos', RESEED, 0);
DBCC CHECKIDENT ('usuarios', RESEED, 0);

-- <<@_______________________________________________________________ CREAR NINOS _______________________________________________________________@>> --
DECLARE @contador INT = 1;
DECLARE @total_ninos INT = 10; -- Número de niños a insertar

WHILE @contador <= @total_ninos
    BEGIN
        -- Generar nombres completos para los niños
        DECLARE @Nombre NVARCHAR(100) =
            (SELECT TOP 1 CONCAT(PrimerNombre, ' ',
                                 CASE WHEN RAND() > 0.5 THEN SegundoNombre + ' ' ELSE '' END,
                                 PrimerApellido, ' ', SegundoApellido)
             FROM (VALUES ('Juan', 'Carlos', 'Pérez', 'García'),
                          ('María', 'Isabel', 'Rodríguez', 'Martínez'),
                          ('Luis', 'Fernando', 'Hernández', 'López'),
                          ('Ana', 'Victoria', 'González', 'Ramírez'),
                          ('Emily', 'Grace', 'Smith', 'Johnson'),
                          ('Oliver', 'James', 'Brown', 'Taylor'),
                          ('Emma', 'Charlotte', 'Davis', 'Wilson'),
                          ('Liam', 'Henry', 'Anderson', 'White'),
                          ('Sophia', 'Amelia', 'Harris', 'Martin'),
                          ('Noah', 'Alexander', 'Thompson', 'Moore'),
                          ('Ava', 'Harper', 'Hall', 'Allen'),
                          ('Lucas', 'Elijah', 'King', 'Wright'),
                          ('Sofía', 'Elena', 'Morales', 'Ríos'),
                          ('Miguel', 'Ángel', 'Fernández', 'Castro'),
                          ('Lucía', 'Valeria', 'Ortega',
                           'Mejía')) AS Nombres(PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido)
             ORDER BY NEWID());

        -- Generar direcciones ficticias para los niños
        DECLARE @Direccion NVARCHAR(256) =
            (SELECT TOP 1 CONCAT(Calle, ' #', CAST((FLOOR(RAND() * 1000) + 1) AS NVARCHAR), ', ', Ciudad)
             FROM (VALUES ('Avenida Principal', 'Ciudad del Sol'),
                          ('Calle Secundaria', 'Villa Alegre'),
                          ('Boulevard Central', 'San Pedro'),
                          ('Calle del Lago', 'Lago Azul'),
                          ('Avenida del Bosque', 'Pinar Norte'),
                          ('Paseo de los Cedros', 'Bosque Alto'),
                          ('Camino Real', 'Ciudad Jardín'),
                          ('Calle del Río', 'River Town'),
                          ('Boulevard de la Paz', 'Serenity City'),
                          ('Camino Antiguo', 'Heritage Town'),
                          ('Calle Diamante', 'Crystal Bay'),
                          ('Avenida del Mar', 'Ocean Breeze'),
                          ('Calle Estrella', 'Starlight City'),
                          ('Boulevard del Sol', 'Sunny Meadows'),
                          ('Calle Primavera', 'Spring Valley'),
                          ('Paseo del Parque', 'Parkside'),
                          ('Avenida Libertad', 'Freedom City'),
                          ('Calle Esmeralda', 'Emerald Town'),
                          ('Boulevard del Norte', 'North Ridge')) AS Direcciones(Calle, Ciudad)
             ORDER BY NEWID());

        -- Generar una cédula costarricense ficticia
        DECLARE @Cedula NVARCHAR(20) = CONCAT(
                CAST(FLOOR(RAND() * 7 + 1) AS NVARCHAR),
                RIGHT('00000000' + CAST(CAST(RAND() * 100000000 AS INT) AS NVARCHAR), 8));

        -- Generar una fecha de nacimiento entre 2018 y 2021
        DECLARE @FechaNacimiento DATE = DATEFROMPARTS(
                FLOOR(RAND() * 4 + 2018), -- Año
                FLOOR(RAND() * 12 + 1), -- Mes
                FLOOR(RAND() * 28 + 1) -- Día
                                        );

        -- Insertar al niño en la tabla ninos
        INSERT INTO ninos (cedula, nombre_nino, fecha_nacimiento, direccion, poliza, activo)
        VALUES (@Cedula, @Nombre, @FechaNacimiento, @Direccion, 'Poliza' + CAST(@contador AS NVARCHAR), 1);

        SET @contador = @contador + 1;
    END;
GO

-- <<@_______________________________________________________________ CREAR USUARIOS _______________________________________________________________@>> --
DECLARE @contador INT = 1;
DECLARE @total_usuarios INT = 10; -- Número de usuarios a crear
DECLARE @rol_usuario INT = 3; -- Rol configurable: 1 = Administrador, 2 = Docente, 3 = Padre

WHILE @contador <= @total_usuarios
    BEGIN
        -- Generar nombres completos para los usuarios
        DECLARE @Nombre NVARCHAR(100) =
            (SELECT TOP 1 CONCAT(PrimerNombre, ' ',
                                 CASE WHEN RAND() > 0.5 THEN SegundoNombre + ' ' ELSE '' END,
                                 PrimerApellido, ' ', SegundoApellido)
             FROM (VALUES ('Juan', 'Carlos', 'Morales', 'Jiménez'),
                          ('María', 'Isabel', 'Hernández', 'López'),
                          ('Luis', 'Fernando', 'Rodríguez', 'Martínez'),
                          ('Ana', 'Victoria', 'Pérez', 'García'),
                          ('Emily', 'Grace', 'Smith', 'Johnson'),
                          ('Oliver', 'James', 'Brown', 'Taylor'),
                          ('Emma', 'Charlotte', 'Davis', 'Wilson'),
                          ('Liam', 'Henry', 'Anderson', 'White'),
                          ('Sophia', 'Amelia', 'Harris', 'Martin'),
                          ('Noah', 'Alexander', 'Thompson', 'Moore'),
                          ('Ava', 'Harper', 'Hall', 'Allen'),
                          ('Lucas', 'Elijah', 'King', 'Wright'),
                          ('Sofía', 'Elena', 'Morales', 'Ríos'),
                          ('Miguel', 'Ángel', 'Fernández', 'Castro'),
                          ('Lucía', 'Valeria', 'Ortega', 'Mejía'),
                          ('Ethan', 'Logan', 'Clark', 'Lewis'),
                          ('Chloe', 'Lily', 'Evans', 'Hill'),
                          ('Zoe', 'Layla', 'Walker', 'Adams'),
                          ('Jack', 'Nathan', 'Scott', 'Phillips'),
                          ('Grace', 'Scarlett', 'Mitchell', 'Campbell'),
                          ('Ella', 'Hannah', 'Carter', 'Parker'),
                          ('Mason', 'William', 'Turner',
                           'Cruz')) AS Nombres(PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido)
             ORDER BY NEWID());

        -- Generar dirección para el usuario
        DECLARE @Direccion NVARCHAR(256) =
            (SELECT TOP 1 CONCAT(Calle, ' #', CAST((FLOOR(RAND() * 1000) + 1) AS NVARCHAR), ', ', Ciudad)
             FROM (VALUES ('Avenida Principal', 'Ciudad del Sol'),
                          ('Calle Secundaria', 'Villa Alegre'),
                          ('Boulevard Central', 'San Pedro'),
                          ('Calle del Lago', 'Lago Azul'),
                          ('Avenida del Bosque', 'Pinar Norte'),
                          ('Paseo de los Cedros', 'Bosque Alto'),
                          ('Camino Real', 'Ciudad Jardín'),
                          ('Calle del Río', 'River Town'),
                          ('Boulevard de la Paz', 'Serenity City'),
                          ('Camino Antiguo', 'Heritage Town'),
                          ('Calle Diamante', 'Crystal Bay'),
                          ('Avenida del Mar', 'Ocean Breeze'),
                          ('Calle Estrella', 'Starlight City'),
                          ('Boulevard del Sol', 'Sunny Meadows'),
                          ('Calle Primavera', 'Spring Valley'),
                          ('Paseo del Parque', 'Parkside')) AS Direcciones(Calle, Ciudad)
             ORDER BY NEWID());

        -- Generar una cédula costarricense ficticia
        DECLARE @Cedula NVARCHAR(20) = CONCAT(
                CAST(FLOOR(RAND() * 7 + 1) AS NVARCHAR),
                RIGHT('00000000' + CAST(CAST(RAND() * 100000000 AS INT) AS NVARCHAR), 8));

        -- Generar un correo electrónico único con dominios aleatorios
        DECLARE @Correo NVARCHAR(100);
        DECLARE @Dominio NVARCHAR(50) =
            (SELECT TOP 1 dominio
             FROM (VALUES ('@gmail.com'),
                          ('@hotmail.com'),
                          ('@yahoo.com'),
                          ('@outlook.com')) AS Dominios(dominio)
             ORDER BY NEWID());
        SET @Correo = LOWER(REPLACE(@Nombre, ' ', '.')) + @Dominio;

        -- Validar unicidad del correo
        WHILE EXISTS (SELECT 1 FROM usuarios WHERE correo_electronico = @Correo)
            BEGIN
                -- Si el correo ya existe, genera uno nuevo añadiendo un número aleatorio
                SET @Correo = LOWER(REPLACE(@Nombre, ' ', '.')) + CAST(FLOOR(RAND() * 1000) AS NVARCHAR) + @Dominio;
            END;

        -- Insertar el usuario en la tabla usuarios con el rol especificado
        INSERT INTO usuarios (cedula, nombre, contrasena_hash, num_Telefono, direccion, correo_electronico, id_rol,
                              activo)
        VALUES (@Cedula, @Nombre,
                'AQAAAAIAAYagAAAAENizrGED/9hel72EeDtNo2830KEPSgmqF0nB4o/+DfN1ws1KlwZR563qQMNW+QJbgA==',
                FLOOR(RAND() * 100000000 + 60000000), @Direccion, @Correo, @rol_usuario, 1);

        SET @contador = @contador + 1;
    END;
GO

-- <<@_______________________________________________________________ CREAR RELACION(PADRES-NINOS) _______________________________________________________________@>> --
-- Variables de control
DECLARE @total_relaciones INT = 10; -- Cantidad total de relaciones a insertar
DECLARE @ninos_por_padre INT = 1; -- Cantidad máxima de niños por padre
DECLARE @relaciones_insertadas INT = 0;
-- Contador de relaciones insertadas

-- Cursor para recorrer los padres válidos
DECLARE padres_cursor CURSOR FOR
    SELECT id_Usuario
    FROM usuarios
    WHERE id_rol = 3
      AND activo = 1;

-- Variables para el cursor
DECLARE @id_padre INT;

-- Abrir el cursor
OPEN padres_cursor;

-- Leer el primer padre
FETCH NEXT FROM padres_cursor INTO @id_padre;

WHILE @@FETCH_STATUS = 0 AND @relaciones_insertadas < @total_relaciones
    BEGIN
        -- Contador de niños asignados por padre
        DECLARE @contador_ninos INT = 0;

        -- Asignar niños al padre hasta alcanzar el límite por padre o el total de relaciones
        WHILE @contador_ninos < @ninos_por_padre AND @relaciones_insertadas < @total_relaciones
            BEGIN
                -- Seleccionar un ID de niño aleatorio que aún no esté relacionado con el padre actual
                DECLARE @id_nino INT = (SELECT TOP 1 id_Nino
                                        FROM ninos
                                        WHERE id_Nino NOT IN
                                              (SELECT id_nino FROM rel_padres_ninos WHERE id_padre = @id_padre)
                                        ORDER BY NEWID());

                -- Verificar si hay un niño disponible
                IF @id_nino IS NOT NULL
                    BEGIN
                        -- Insertar la relación entre el padre y el niño
                        INSERT INTO rel_padres_ninos (id_padre, id_nino, relacion)
                        VALUES (@id_padre, @id_nino, 'Padre');

                        -- Incrementar contadores
                        SET @contador_ninos = @contador_ninos + 1;
                        SET @relaciones_insertadas = @relaciones_insertadas + 1;
                    END;
                ELSE
                    BEGIN
                        -- No hay más niños disponibles para este padre
                        BREAK;
                    END;
            END;

        -- Leer el siguiente padre
        FETCH NEXT FROM padres_cursor INTO @id_padre;
    END;

-- Cerrar y liberar el cursor
CLOSE padres_cursor;
DEALLOCATE padres_cursor;

-- Mostrar resultados
PRINT CONCAT('Relaciones insertadas: ', @relaciones_insertadas);
GO

