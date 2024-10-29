--------------------------------- Crear y usar la base de datos ---------------------------------
CREATE DATABASE kinder_care;
GO

USE kinder_care;
GO

--------------------------------- Tabla de Roles ---------------------------------
CREATE TABLE roles
(
    id_Rol INT PRIMARY KEY IDENTITY (1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE
);
GO

--------------------------------- Tabla de Usuarios ---------------------------------
CREATE TABLE usuarios
(
    id_Usuario           INT PRIMARY KEY IDENTITY (1,1),
    cedula               VARCHAR(20)  NOT NULL,
    nombre               VARCHAR(100) NOT NULL,
    contrasena_hash      VARCHAR(100) NOT NULL,
    num_Telefono         INT,
    direccion            VARCHAR(256) NOT NULL,
    correo_electronico   VARCHAR(100) NOT NULL,
    id_rol               INT          NOT NULL,
    fecha_creacion       DATETIME     DEFAULT GETDATE(),
    ultima_actualizacion DATETIME     DEFAULT GETDATE(),
    activo               BIT          DEFAULT 1,
    TokenRecovery        VARCHAR(100) DEFAULT '0',
    CONSTRAINT fk_usuarios_rol FOREIGN KEY (id_rol) REFERENCES roles (id_Rol)
);
GO

CREATE TRIGGER trg_update_audit_usuarios
    ON usuarios
    AFTER UPDATE
    AS
BEGIN
    UPDATE usuarios
    SET ultima_actualizacion = GETDATE()
    WHERE id_Usuario IN (SELECT id_Usuario FROM inserted);
END;
GO

--------------------------------- Tabla de Niños ---------------------------------
CREATE TABLE ninos
(
    id_Nino              INT PRIMARY KEY IDENTITY (1,1),
    cedula               VARCHAR(20)  NOT NULL,
    nombre_nino          VARCHAR(100) NOT NULL,
    fecha_nacimiento     DATE         NOT NULL,
    direccion            VARCHAR(256) NOT NULL,
    poliza               VARCHAR(100),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    activo               BIT      DEFAULT 1
);
GO

CREATE TRIGGER trg_update_audit_ninos
    ON ninos
    AFTER UPDATE
    AS
BEGIN
    UPDATE ninos
    SET ultima_actualizacion = GETDATE()
    WHERE id_Nino IN (SELECT id_Nino FROM inserted);
END;
GO

--------------------------------- Tabla de Relación Padre(Usuario)-Niño ---------------------------------
CREATE TABLE rel_padres_ninos
(
    id_padre INT         NOT NULL,
    id_nino  INT         NOT NULL,
    relacion VARCHAR(50) NOT NULL,
    PRIMARY KEY (id_padre, id_nino),
    CONSTRAINT fk_padres_ninos_padre FOREIGN KEY (id_padre) REFERENCES Usuarios (id_Usuario),
    CONSTRAINT fk_padres_ninos_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino)
);
GO

--------------------------------- Tabla de Docentes ---------------------------------
CREATE TABLE docentes
(
    id_Docente           INT PRIMARY KEY IDENTITY (1,1),
    id_usuario           INT NOT NULL, -- referencia al usuario que es docente
    fecha_nacimiento     DATE,
    grupo_asignado       VARCHAR(100),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    activo               BIT      DEFAULT 1,
    CONSTRAINT fk_docentes_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id_Usuario)
);
GO

CREATE TRIGGER trg_update_audit_docentes
    ON docentes
    AFTER UPDATE
    AS
BEGIN
    UPDATE docentes
    SET ultima_actualizacion = GETDATE()
    WHERE id_Docente IN (SELECT id_Docente FROM inserted);
END;
GO

CREATE TRIGGER trg_insert_docente
    ON usuarios
    AFTER INSERT
    AS
BEGIN
    INSERT INTO docentes (id_usuario, fecha_creacion, ultima_actualizacion)
    SELECT id_Usuario, GETDATE(), GETDATE()
    FROM inserted
    WHERE id_rol = 2; -- Rol 2 corresponde a docentes
END;
GO

CREATE TRIGGER trg_update_Rol_Docente
    ON usuarios
    AFTER UPDATE
    AS
BEGIN
    SET NOCOUNT ON;

    -- Eliminar el registro de docente si el rol del usuario se ha cambiado a algo diferente a docente
    DELETE
    FROM docentes
    WHERE id_usuario IN (SELECT id_usuario FROM inserted WHERE id_rol <> 2);

    -- Insertar o actualizar el registro en docentes si el rol del usuario se ha cambiado a docente
    MERGE INTO docentes AS target
    USING (SELECT id_usuario FROM inserted WHERE id_rol = 2) AS source
    ON target.id_usuario = source.id_usuario
    WHEN NOT MATCHED THEN
        INSERT (id_usuario, fecha_nacimiento, grupo_asignado, fecha_creacion, ultima_actualizacion)
        VALUES (source.id_usuario, NULL, NULL, GETDATE(), GETDATE())
    WHEN MATCHED THEN
        UPDATE SET ultima_actualizacion = GETDATE(); -- Actualiza la fecha de última actualización si ya existe
END;
GO

--------------------------------- Tabla de Progreso Académico ---------------------------------
CREATE TABLE progreso_academico
(
    id_Progreso_Academico INT PRIMARY KEY IDENTITY (1,1),
    id_nino               INT          NOT NULL,
    area_academica        VARCHAR(100) NOT NULL,
    nivel_progreso        VARCHAR(50),
    descripcion           VARCHAR(256),
    fecha_creacion        DATETIME DEFAULT GETDATE(),
    ultima_actualizacion  DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_progreso_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino)
);
GO

CREATE TRIGGER trg_update_audit_progreso_academico
    ON progreso_academico
    AFTER UPDATE
    AS
BEGIN
    UPDATE progreso_academico
    SET ultima_actualizacion = GETDATE()
    WHERE id_Progreso_Academico IN (SELECT id_Progreso_Academico FROM inserted);
END;
GO

--------------------------------- Tabla de Evaluaciones ---------------------------------
CREATE TABLE evaluaciones
(
    id_Evaluacion        INT PRIMARY KEY IDENTITY (1,1),
    id_nino              INT          NOT NULL,
    asignatura           VARCHAR(100) NOT NULL,
    puntaje              DECIMAL(5, 2) CHECK (puntaje BETWEEN 0 AND 100),
    fecha                DATE         NOT NULL,
    comentarios          VARCHAR(256),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_evaluaciones_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino)
);
GO

CREATE TRIGGER trg_update_audit_evaluaciones
    ON evaluaciones
    AFTER UPDATE
    AS
BEGIN
    UPDATE evaluaciones
    SET ultima_actualizacion = GETDATE()
    WHERE id_Evaluacion IN (SELECT id_Evaluacion FROM inserted);
END;
GO

--------------------------------- Tabla de Observaciones de Docentes ---------------------------------
CREATE TABLE observaciones_docentes
(
    id_Observacion_Docente INT PRIMARY KEY IDENTITY (1,1),
    id_nino                INT         NOT NULL,
    id_docente             INT         NOT NULL,
    tipo                   VARCHAR(50) NOT NULL,
    descripcion            VARCHAR(256),
    fecha                  DATE        NOT NULL,
    CONSTRAINT fk_observacion_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    CONSTRAINT fk_observacion_docente FOREIGN KEY (id_docente) REFERENCES docentes (id_Docente)
);
GO

--------------------------------- Tabla de Asistencia ---------------------------------
CREATE TABLE asistencia
(
    id_Asistencia INT PRIMARY KEY IDENTITY (1,1),
    id_nino       INT  NOT NULL,
    fecha         DATE NOT NULL,
    hora_entrada  TIME,
    hora_salida   TIME,
    estado        VARCHAR(20) CHECK (estado IN ('Presente', 'Ausente', 'Tarde')),
    CONSTRAINT fk_asistencia_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino)
);
GO


--------------------------------- Tablas de Pagos ---------------------------------
CREATE TABLE tipo_pagos
(
    id_tipo_pago     INT PRIMARY KEY IDENTITY (1,1),
    nombre_tipo_pago VARCHAR(50) NOT NULL UNIQUE,
    descripcion      VARCHAR(255),
    activo           BIT DEFAULT 1
);
GO


CREATE TABLE pagos
(
    id_Pago              INT PRIMARY KEY IDENTITY (1,1),
    id_nino              INT            NOT NULL,
    id_padre             INT            NOT NULL,
    id_tipo_pago         INT            NOT NULL,
    fecha_pago           DATE           NOT NULL,
    monto                DECIMAL(10, 2) NOT NULL,
    metodo_pago          VARCHAR(50),
    referencia_factura   VARCHAR(255),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_pagos_nino FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    CONSTRAINT fk_pagos_padre FOREIGN KEY (id_padre) REFERENCES usuarios (id_Usuario),
    CONSTRAINT fk_pagos_tipo FOREIGN KEY (id_tipo_pago) REFERENCES tipo_pagos (id_Tipo_Pago)
);
GO

CREATE TRIGGER trg_update_audit_pagos
    ON pagos
    AFTER UPDATE
    AS
BEGIN
    UPDATE pagos
    SET ultima_actualizacion = GETDATE()
    WHERE id_Pago IN (SELECT id_Pago FROM inserted);
END;
GO

--------------------------------- Relación Docente-Niño-Materia ---------------------------------
CREATE TABLE rel_docente_nino_materia
(
    id_docente INT          NOT NULL,
    id_nino    INT          NOT NULL,
    materia    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id_docente, id_nino),
    FOREIGN KEY (id_docente) REFERENCES docentes (id_Docente),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino)
);
GO

--------------------------------- Tabla de Contactos de Emergencia ---------------------------------
CREATE TABLE contactos_emergencia
(
    id_Contacto_Emergencia INT PRIMARY KEY IDENTITY (1,1),
    nombre_contacto        VARCHAR(100) NOT NULL,
    relacion               VARCHAR(50),
    telefono               INT,
    direccion              VARCHAR(255),
    activo                 BIT DEFAULT 1
);
GO

--------------------------------- Relación Niño-Contacto de Emergencia ---------------------------------
CREATE TABLE rel_nino_contacto_emergencia
(
    id_nino     INT NOT NULL,
    id_contacto INT NOT NULL,
    PRIMARY KEY (id_nino, id_contacto),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_nino),
    FOREIGN KEY (id_contacto) REFERENCES contactos_emergencia (id_Contacto_Emergencia)
);
GO

--------------------------------- Tabla de Alergias ---------------------------------
CREATE TABLE alergias
(
    id_Alergia     INT PRIMARY KEY IDENTITY (1,1),
    nombre_alergia VARCHAR(100) NOT NULL,
    activo         BIT DEFAULT 1
);
GO

--------------------------------- Tabla de Condiciones Médicas ---------------------------------
CREATE TABLE condiciones_medicas
(
    id_Condicion_medica INT PRIMARY KEY IDENTITY (1,1),
    nombre_condicion    VARCHAR(100) NOT NULL,
    activo              BIT DEFAULT 1
);
GO

--------------------------------- Tabla de Medicamentos ---------------------------------
CREATE TABLE medicamentos
(
    id_Medicamento     INT PRIMARY KEY IDENTITY (1,1),
    nombre_medicamento VARCHAR(100) NOT NULL,
    dosis              VARCHAR(50)  NOT NULL,
    activo             BIT DEFAULT 1
);
GO

--------------------------------- Relación Niño-Alergias ---------------------------------
CREATE TABLE rel_nino_alergia
(
    id_nino    INT NOT NULL,
    id_alergia INT NOT NULL,
    PRIMARY KEY (id_nino, id_alergia),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    FOREIGN KEY (id_alergia) REFERENCES alergias (id_Alergia)
);
GO

--------------------------------- Relación Niño-Condiciones Médicas ---------------------------------
CREATE TABLE rel_nino_condicion
(
    id_nino      INT NOT NULL,
    id_condicion INT NOT NULL,
    PRIMARY KEY (id_nino, id_condicion),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    FOREIGN KEY (id_condicion) REFERENCES condiciones_medicas (id_Condicion_Medica)
);
GO

--------------------------------- Relación Niño-Medicamentos ---------------------------------
CREATE TABLE rel_nino_medicamento
(
    id_nino        INT NOT NULL,
    id_medicamento INT NOT NULL,
    PRIMARY KEY (id_nino, id_medicamento),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    FOREIGN KEY (id_medicamento) REFERENCES medicamentos (id_Medicamento)
);
GO

--------------------------------- Tablas de Actividades ---------------------------------
CREATE TABLE tipo_actividad
(
    id_Tipo_Actividad     INT PRIMARY KEY IDENTITY (1,1),
    nombre_tipo_actividad VARCHAR(50) NOT NULL UNIQUE,
    activo                BIT DEFAULT 1
);
GO

CREATE TABLE actividades
(
    id_Actividad      INT PRIMARY KEY IDENTITY (1,1),
    id_tipo_actividad INT  NOT NULL,
    fecha             DATE NOT NULL,
    lugar             VARCHAR(100),
    activo            BIT DEFAULT 1,
    FOREIGN KEY (id_tipo_actividad) REFERENCES tipo_actividad (id_Tipo_Actividad)
);
GO

--------------------------------- Relación Niño-Actividades ---------------------------------
CREATE TABLE rel_nino_actividad
(
    id_nino      INT                                                                NOT NULL,
    id_actividad INT                                                                NOT NULL,
    asistencia   VARCHAR(50) CHECK (asistencia IN ('Presente', 'Ausente', 'Tarde')) NOT NULL,
    PRIMARY KEY (id_nino, id_actividad),
    FOREIGN KEY (id_nino) REFERENCES ninos (id_Nino),
    FOREIGN KEY (id_actividad) REFERENCES actividades (id_Actividad)
);
GO

--------------------------------- Indices ---------------------------------
CREATE INDEX idx_usuarios_rol ON usuarios (id_rol);
GO

CREATE INDEX idx_ninos_nombre ON ninos (nombre_nino);
GO

CREATE INDEX idx_pagos_padre ON pagos (id_padre);
GO

CREATE INDEX idx_evaluaciones_nino ON evaluaciones (id_nino);
GO

CREATE INDEX idx_asistencia_nino ON asistencia (id_nino);
GO

CREATE INDEX idx_rel_padres_ninos_nino ON rel_padres_ninos (id_nino);
GO
