--------------------------------- Crear y usar la base de datos ---------------------------------
CREATE DATABASE kinder_care;
GO

USE kinder_care;
GO

--------------------------------- Tabla de Roles ---------------------------------
CREATE TABLE roles
(
    id     INT PRIMARY KEY IDENTITY (1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
);

--------------------------------- Tabla de Usuarios ---------------------------------
CREATE TABLE usuarios
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    nombre               VARCHAR(100) NOT NULL,
    contrasena_hash      VARCHAR(100) NOT NULL,
    id_rol               INT          NOT NULL,
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    activo               BIT      DEFAULT 1,
    CONSTRAINT fk_usuarios_rol FOREIGN KEY (id_rol) REFERENCES roles (id)
);
GO

CREATE TRIGGER trg_update_audit_usuarios
    ON usuarios
    AFTER UPDATE
    AS
BEGIN
    UPDATE usuarios
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Niños ---------------------------------
CREATE TABLE ninos
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    nombre_nino          VARCHAR(100) NOT NULL,
    fecha_nacimiento     DATE         NOT NULL,
    direccion            TEXT         NOT NULL,
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
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Padres ---------------------------------
CREATE TABLE padres
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    id_usuario           INT NOT NULL,
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    activo               BIT      DEFAULT 1,
    CONSTRAINT fk_padres_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id)
);
GO

CREATE TRIGGER trg_update_audit_padres
    ON padres
    AFTER UPDATE
    AS
BEGIN
    UPDATE padres
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Relación Padre-Niño ---------------------------------
CREATE TABLE rel_padres_ninos
(
    id_padre INT         NOT NULL,
    id_nino  INT         NOT NULL,
    relacion VARCHAR(50) NOT NULL,
    PRIMARY KEY (id_padre, id_nino),
    CONSTRAINT fk_padres_ninos_padre FOREIGN KEY (id_padre) REFERENCES padres (id),
    CONSTRAINT fk_padres_ninos_nino FOREIGN KEY (id_nino) REFERENCES ninos (id)
);

--------------------------------- Tabla de Docentes ---------------------------------
CREATE TABLE docentes
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    id_usuario           INT          NOT NULL,
    nombre               VARCHAR(100) NOT NULL,
    apellido             VARCHAR(100) NOT NULL,
    fecha_nacimiento     DATE         NOT NULL,
    direccion            TEXT         NOT NULL,
    telefono             VARCHAR(15),
    correo_electronico   VARCHAR(100) NOT NULL,
    grupo_asignado       VARCHAR(100),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    activo               BIT      DEFAULT 1,
    CONSTRAINT fk_docentes_usuario FOREIGN KEY (id_usuario) REFERENCES usuarios (id)
);
GO

CREATE TRIGGER trg_update_audit_docentes
    ON docentes
    AFTER UPDATE
    AS
BEGIN
    UPDATE docentes
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Progreso Académico ---------------------------------
CREATE TABLE progreso_academico
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    id_nino              INT          NOT NULL,
    area_academica       VARCHAR(100) NOT NULL,
    nivel_progreso       VARCHAR(50),
    descripcion          TEXT,
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_progreso_nino FOREIGN KEY (id_nino) REFERENCES ninos (id)
);
GO

CREATE TRIGGER trg_update_audit_progreso_academico
    ON progreso_academico
    AFTER UPDATE
    AS
BEGIN
    UPDATE progreso_academico
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Evaluaciones ---------------------------------
CREATE TABLE evaluaciones
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    id_nino              INT          NOT NULL,
    asignatura           VARCHAR(100) NOT NULL,
    puntaje              DECIMAL(5, 2) CHECK (puntaje BETWEEN 0 AND 100),
    fecha                DATE         NOT NULL,
    comentarios          TEXT,
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_evaluaciones_nino FOREIGN KEY (id_nino) REFERENCES ninos (id)
);
GO

CREATE TRIGGER trg_update_audit_evaluaciones
    ON evaluaciones
    AFTER UPDATE
    AS
BEGIN
    UPDATE evaluaciones
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Tabla de Observaciones de Docentes ---------------------------------
CREATE TABLE observaciones_docentes
(
    id          INT PRIMARY KEY IDENTITY (1,1),
    id_nino     INT         NOT NULL,
    id_docente  INT         NOT NULL,
    tipo        VARCHAR(50) NOT NULL,
    descripcion TEXT,
    fecha       DATE        NOT NULL,
    CONSTRAINT fk_observacion_nino FOREIGN KEY (id_nino) REFERENCES ninos (id),
    CONSTRAINT fk_observacion_docente FOREIGN KEY (id_docente) REFERENCES docentes (id)
);

--------------------------------- Tabla de Asistencia ---------------------------------
CREATE TABLE asistencia
(
    id           INT PRIMARY KEY IDENTITY (1,1),
    id_nino      INT  NOT NULL,
    fecha        DATE NOT NULL,
    hora_entrada TIME,
    hora_salida  TIME,
    estado       VARCHAR(20) CHECK (estado IN ('Presente', 'Ausente', 'Tarde')),
    CONSTRAINT fk_asistencia_nino FOREIGN KEY (id_nino) REFERENCES ninos (id)
);


--------------------------------- Tablas de Pagos ---------------------------------
CREATE TABLE tipo_pagos
(
    id               INT PRIMARY KEY IDENTITY (1,1),
    nombre_tipo_pago VARCHAR(50) NOT NULL UNIQUE,
    descripcion      VARCHAR(255),
    activo           BIT DEFAULT 1
);


CREATE TABLE pagos
(
    id                   INT PRIMARY KEY IDENTITY (1,1),
    id_nino              INT            NOT NULL,
    id_padre             INT            NOT NULL,
    id_tipo_pago         INT            NOT NULL,
    fecha_pago           DATE           NOT NULL,
    monto                DECIMAL(10, 2) NOT NULL,
    metodo_pago          VARCHAR(50),
    referencia_factura   VARCHAR(255),
    fecha_creacion       DATETIME DEFAULT GETDATE(),
    ultima_actualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_pagos_nino FOREIGN KEY (id_nino) REFERENCES ninos (id),
    CONSTRAINT fk_pagos_padre FOREIGN KEY (id_padre) REFERENCES padres (id),
    CONSTRAINT fk_pagos_tipo FOREIGN KEY (id_tipo_pago) REFERENCES tipo_pagos (id)
);
GO

CREATE TRIGGER trg_update_audit_pagos
    ON pagos
    AFTER UPDATE
    AS
BEGIN
    UPDATE pagos
    SET ultima_actualizacion = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

--------------------------------- Relación Docente-Niño-Materia ---------------------------------
CREATE TABLE rel_docente_nino_materia
(
    id_docente INT          NOT NULL,
    id_nino    INT          NOT NULL,
    materia    VARCHAR(100) NOT NULL,
    PRIMARY KEY (id_docente, id_nino),
    FOREIGN KEY (id_docente) REFERENCES docentes (id),
    FOREIGN KEY (id_nino) REFERENCES ninos (id)
);


--------------------------------- Tabla de Contactos de Emergencia ---------------------------------
CREATE TABLE contactos_emergencia
(
    id              INT PRIMARY KEY IDENTITY (1,1),
    nombre_contacto VARCHAR(100) NOT NULL,
    relacion        VARCHAR(50),
    telefono        VARCHAR(15) CHECK (LEN(telefono) = 8),
    direccion       VARCHAR(255),
    activo          BIT DEFAULT 1
);

--------------------------------- Relación Niño-Contacto de Emergencia ---------------------------------
CREATE TABLE rel_nino_contacto_emergencia
(
    id_nino     INT NOT NULL,
    id_contacto INT NOT NULL,
    PRIMARY KEY (id_nino, id_contacto),
    FOREIGN KEY (id_nino) REFERENCES ninos (id),
    FOREIGN KEY (id_contacto) REFERENCES contactos_emergencia (id)
);

--------------------------------- Tabla de Alergias ---------------------------------
CREATE TABLE alergias
(
    id             INT PRIMARY KEY IDENTITY (1,1),
    nombre_alergia VARCHAR(100) NOT NULL,
    activo         BIT DEFAULT 1
);

--------------------------------- Tabla de Condiciones Médicas ---------------------------------
CREATE TABLE condiciones_medicas
(
    id               INT PRIMARY KEY IDENTITY (1,1),
    nombre_condicion VARCHAR(100) NOT NULL,
    activo           BIT DEFAULT 1
);

--------------------------------- Tabla de Medicamentos ---------------------------------
CREATE TABLE medicamentos
(
    id                 INT PRIMARY KEY IDENTITY (1,1),
    nombre_medicamento VARCHAR(100) NOT NULL,
    dosis              VARCHAR(50)  NOT NULL,
    activo             BIT DEFAULT 1
);

--------------------------------- Relación Niño-Alergias ---------------------------------
CREATE TABLE rel_nino_alergia
(
    id_nino    INT NOT NULL,
    id_alergia INT NOT NULL,
    PRIMARY KEY (id_nino, id_alergia),
    FOREIGN KEY (id_nino) REFERENCES ninos (id),
    FOREIGN KEY (id_alergia) REFERENCES alergias (id)
);

--------------------------------- Relación Niño-Condiciones Médicas ---------------------------------
CREATE TABLE rel_nino_condicion
(
    id_nino      INT NOT NULL,
    id_condicion INT NOT NULL,
    PRIMARY KEY (id_nino, id_condicion),
    FOREIGN KEY (id_nino) REFERENCES ninos (id),
    FOREIGN KEY (id_condicion) REFERENCES condiciones_medicas (id)
);

--------------------------------- Relación Niño-Medicamentos ---------------------------------
CREATE TABLE rel_nino_medicamento
(
    id_nino        INT NOT NULL,
    id_medicamento INT NOT NULL,
    PRIMARY KEY (id_nino, id_medicamento),
    FOREIGN KEY (id_nino) REFERENCES ninos (id),
    FOREIGN KEY (id_medicamento) REFERENCES medicamentos (id)
);

--------------------------------- Tablas de Actividades ---------------------------------
CREATE TABLE tipo_actividad
(
    id                    INT PRIMARY KEY IDENTITY (1,1),
    nombre_tipo_actividad VARCHAR(50) NOT NULL UNIQUE,
    activo                BIT DEFAULT 1
);

CREATE TABLE actividades
(
    id                INT PRIMARY KEY IDENTITY (1,1),
    id_tipo_actividad INT  NOT NULL,
    fecha             DATE NOT NULL,
    lugar             VARCHAR(100),
    activo            BIT DEFAULT 1,
    FOREIGN KEY (id_tipo_actividad) REFERENCES tipo_actividad (id)
);

--------------------------------- Relación Niño-Actividades ---------------------------------
CREATE TABLE rel_nino_actividad
(
    id_nino      INT                                                                NOT NULL,
    id_actividad INT                                                                NOT NULL,
    asistencia   VARCHAR(50) CHECK (asistencia IN ('Presente', 'Ausente', 'Tarde')) NOT NULL,
    PRIMARY KEY (id_nino, id_actividad),
    FOREIGN KEY (id_nino) REFERENCES ninos (id),
    FOREIGN KEY (id_actividad) REFERENCES actividades (id)
);

--------------------------------- Indices ---------------------------------
CREATE INDEX idx_usuarios_rol ON usuarios (id_rol);
CREATE INDEX idx_ninos_nombre ON ninos (nombre_nino);
CREATE INDEX idx_padres_usuario ON padres (id_usuario);
CREATE INDEX idx_pagos_padre ON pagos (id_padre);
CREATE INDEX idx_evaluaciones_nino ON evaluaciones (id_nino);
CREATE INDEX idx_asistencia_nino ON asistencia (id_nino);
CREATE INDEX idx_rel_padres_ninos_nino ON rel_padres_ninos (id_nino);
