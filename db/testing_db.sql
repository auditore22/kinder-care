-- Vista de Usuarios por Rol
SELECT *
FROM vw_usuarios_por_rol;

-- Vista de Niños
SELECT *
FROM vw_ninos_por_nombre;

-- Vista de Padres
SELECT *
FROM vw_padres_con_usuario;

-- Vista de Pagos
SELECT *
FROM vw_pagos_por_padre;

-- Vista de Evaluaciones 
SELECT *
FROM vw_evaluaciones_por_nino;

-- Vista de Asistencia
SELECT *
FROM vw_asistencia_por_nino;

-- Vista de Relación de Padres con Niños
SELECT *
FROM vw_rel_padres_ninos;

-- Vista de Contactos de Emergencia 
SELECT *
FROM vw_contactos_emergencia_por_nino;

-- Vista para obtener una lista de actividades 
SELECT *
FROM vw_actividades_nino;

-- Vista para generar reportes de progreso académico 
SELECT *
FROM vw_reporte_progreso_academico;

-- Vista de Niños con Condiciones Médicas
SELECT *
FROM vw_ninos_condiciones_medicas;

-- Vista de Niños con Alergias
SELECT *
FROM vw_ninos_alergias;

-- Vista de Niños con Medicamentos
SELECT *
FROM vw_ninos_medicamentos;

-- Vista para consultar todos los profesores que enseñan
SELECT *
FROM vw_profesores_por_nino;

-- Vista para ver todos los niños a los que un docente enseña
SELECT *
FROM vw_ninos_por_docente;

-- Vista expediente
SELECT *
FROM vw_expediente;
