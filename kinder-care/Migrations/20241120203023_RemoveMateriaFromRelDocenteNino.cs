using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kinder_care.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMateriaFromRelDocenteNino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alergias",
                columns: table => new
                {
                    id_Alergia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_alergia = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__alergias__148C09CA3541F7AD", x => x.id_Alergia);
                });

            migrationBuilder.CreateTable(
                name: "condiciones_medicas",
                columns: table => new
                {
                    id_Condicion_medica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_condicion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__condicio__AF5CC79BA27B8280", x => x.id_Condicion_medica);
                });

            migrationBuilder.CreateTable(
                name: "contactos_emergencia",
                columns: table => new
                {
                    id_Contacto_Emergencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_contacto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    relacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<int>(type: "int", nullable: true),
                    direccion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contacto__B407E9F29A28ED1F", x => x.id_Contacto_Emergencia);
                });

            migrationBuilder.CreateTable(
                name: "medicamentos",
                columns: table => new
                {
                    id_Medicamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_medicamento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    dosis = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicame__DA5E04EB0503C2D4", x => x.id_Medicamento);
                });

            migrationBuilder.CreateTable(
                name: "ninos",
                columns: table => new
                {
                    id_Nino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    nombre_nino = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    direccion = table.Column<string>(type: "text", nullable: false),
                    poliza = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ninos__3CAF0674B917C51C", x => x.id_Nino);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__76482FD2122BF699", x => x.id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "tipo_actividad",
                columns: table => new
                {
                    id_Tipo_Actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_tipo_actividad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tipo_act__03B324327EFE116E", x => x.id_Tipo_Actividad);
                });

            migrationBuilder.CreateTable(
                name: "tipo_pagos",
                columns: table => new
                {
                    id_tipo_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_tipo_pago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tipo_pag__F7E781E5D145505F", x => x.id_tipo_pago);
                });

            migrationBuilder.CreateTable(
                name: "asistencia",
                columns: table => new
                {
                    id_Asistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    hora_entrada = table.Column<TimeOnly>(type: "time", nullable: true),
                    hora_salida = table.Column<TimeOnly>(type: "time", nullable: true),
                    estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asistenc__AFB810EF003C5B5E", x => x.id_Asistencia);
                    table.ForeignKey(
                        name: "fk_asistencia_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "evaluaciones",
                columns: table => new
                {
                    id_Evaluacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    asignatura = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    puntaje = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    comentarios = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__evaluaci__6E22DFF602DC711F", x => x.id_Evaluacion);
                    table.ForeignKey(
                        name: "fk_evaluaciones_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "progreso_academico",
                columns: table => new
                {
                    id_Progreso_Academico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    area_academica = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nivel_progreso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__progreso__20F2A38CE3214FF3", x => x.id_Progreso_Academico);
                    table.ForeignKey(
                        name: "fk_progreso_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "rel_nino_alergia",
                columns: table => new
                {
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_alergia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_nino_alergia", x => new { x.id_nino, x.id_alergia });
                    table.ForeignKey(
                        name: "FK_rel_nino_alergia_alergias_id_alergia",
                        column: x => x.id_alergia,
                        principalTable: "alergias",
                        principalColumn: "id_Alergia");
                    table.ForeignKey(
                        name: "FK_rel_nino_alergia_ninos_id_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "rel_nino_condicion",
                columns: table => new
                {
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_condicion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_nino_condicion", x => new { x.id_nino, x.id_condicion });
                    table.ForeignKey(
                        name: "FK_rel_nino_condicion_condiciones_medicas_id_condicion",
                        column: x => x.id_condicion,
                        principalTable: "condiciones_medicas",
                        principalColumn: "id_Condicion_medica");
                    table.ForeignKey(
                        name: "FK_rel_nino_condicion_ninos_id_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "rel_nino_contacto_emergencia",
                columns: table => new
                {
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_contacto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_nino_contacto_emergencia", x => new { x.id_nino, x.id_contacto });
                    table.ForeignKey(
                        name: "FK_rel_nino_contacto_emergencia_contactos_emergencia_id_contacto",
                        column: x => x.id_contacto,
                        principalTable: "contactos_emergencia",
                        principalColumn: "id_Contacto_Emergencia");
                    table.ForeignKey(
                        name: "FK_rel_nino_contacto_emergencia_ninos_id_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "rel_nino_medicamento",
                columns: table => new
                {
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_medicamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_nino_medicamento", x => new { x.id_nino, x.id_medicamento });
                    table.ForeignKey(
                        name: "FK_rel_nino_medicamento_medicamentos_id_medicamento",
                        column: x => x.id_medicamento,
                        principalTable: "medicamentos",
                        principalColumn: "id_Medicamento");
                    table.ForeignKey(
                        name: "FK_rel_nino_medicamento_ninos_id_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    contrasena_hash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    num_Telefono = table.Column<int>(type: "int", nullable: false),
                    direccion = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    correo_electronico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TokenRecovery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__8E901EAA42E54E3E", x => x.id_Usuario);
                    table.ForeignKey(
                        name: "fk_usuarios_rol",
                        column: x => x.id_rol,
                        principalTable: "roles",
                        principalColumn: "id_Rol");
                });

            migrationBuilder.CreateTable(
                name: "actividades",
                columns: table => new
                {
                    id_Actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_tipo_actividad = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    lugar = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__activida__071C9F95FDAD0639", x => x.id_Actividad);
                    table.ForeignKey(
                        name: "FK__actividad__id_ti__02FC7413",
                        column: x => x.id_tipo_actividad,
                        principalTable: "tipo_actividad",
                        principalColumn: "id_Tipo_Actividad");
                });

            migrationBuilder.CreateTable(
                name: "docentes",
                columns: table => new
                {
                    id_Docente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    grupo_asignado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__docentes__EBE50C3E0B7E379A", x => x.id_Docente);
                    table.ForeignKey(
                        name: "fk_docentes_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_Usuario");
                });

            migrationBuilder.CreateTable(
                name: "pagos",
                columns: table => new
                {
                    id_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_padre = table.Column<int>(type: "int", nullable: false),
                    id_tipo_pago = table.Column<int>(type: "int", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    metodo_pago = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    referencia_factura = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pagos__2A3B86B22059D92F", x => x.id_Pago);
                    table.ForeignKey(
                        name: "FK_pagos_ninos_id_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pagos_tipo_pagos_id_tipo_pago",
                        column: x => x.id_tipo_pago,
                        principalTable: "tipo_pagos",
                        principalColumn: "id_tipo_pago",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pagos_usuarios_id_padre",
                        column: x => x.id_padre,
                        principalTable: "usuarios",
                        principalColumn: "id_Usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rel_padres_ninos",
                columns: table => new
                {
                    id_padre = table.Column<int>(type: "int", nullable: false),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    relacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rel_padr__42228E4E09B5059C", x => new { x.id_padre, x.id_nino });
                    table.ForeignKey(
                        name: "fk_padres_ninos_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                    table.ForeignKey(
                        name: "fk_padres_ninos_padre",
                        column: x => x.id_padre,
                        principalTable: "usuarios",
                        principalColumn: "id_Usuario");
                });

            migrationBuilder.CreateTable(
                name: "rel_nino_actividad",
                columns: table => new
                {
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_actividad = table.Column<int>(type: "int", nullable: false),
                    asistencia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rel_nino__06C41D3E23CF66C7", x => new { x.id_nino, x.id_actividad });
                    table.ForeignKey(
                        name: "FK__rel_nino___id_ac__07C12930",
                        column: x => x.id_actividad,
                        principalTable: "actividades",
                        principalColumn: "id_Actividad");
                    table.ForeignKey(
                        name: "FK__rel_nino___id_ni__06CD04F7",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "observaciones_docentes",
                columns: table => new
                {
                    id_Observacion_Docente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nino = table.Column<int>(type: "int", nullable: false),
                    id_docente = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__observac__FCB6ACAC58572707", x => x.id_Observacion_Docente);
                    table.ForeignKey(
                        name: "fk_observacion_docente",
                        column: x => x.id_docente,
                        principalTable: "docentes",
                        principalColumn: "id_Docente");
                    table.ForeignKey(
                        name: "fk_observacion_nino",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateTable(
                name: "rel_docente_nino_materia",
                columns: table => new
                {
                    id_docente = table.Column<int>(type: "int", nullable: false),
                    id_nino = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__rel_doce__42BD208A0B7446B8", x => new { x.id_docente, x.id_nino });
                    table.ForeignKey(
                        name: "FK__rel_docen__id_do__5FB337D6",
                        column: x => x.id_docente,
                        principalTable: "docentes",
                        principalColumn: "id_Docente");
                    table.ForeignKey(
                        name: "FK__rel_docen__id_ni__60A75C0F",
                        column: x => x.id_nino,
                        principalTable: "ninos",
                        principalColumn: "id_Nino");
                });

            migrationBuilder.CreateIndex(
                name: "IX_actividades_id_tipo_actividad",
                table: "actividades",
                column: "id_tipo_actividad");

            migrationBuilder.CreateIndex(
                name: "idx_asistencia_nino",
                table: "asistencia",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "IX_docentes_id_usuario",
                table: "docentes",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_evaluaciones_nino",
                table: "evaluaciones",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "idx_ninos_nombre",
                table: "ninos",
                column: "nombre_nino");

            migrationBuilder.CreateIndex(
                name: "IX_observaciones_docentes_id_docente",
                table: "observaciones_docentes",
                column: "id_docente");

            migrationBuilder.CreateIndex(
                name: "IX_observaciones_docentes_id_nino",
                table: "observaciones_docentes",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "idx_pagos_padre",
                table: "pagos",
                column: "id_padre");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_id_nino",
                table: "pagos",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "IX_pagos_id_tipo_pago",
                table: "pagos",
                column: "id_tipo_pago");

            migrationBuilder.CreateIndex(
                name: "IX_progreso_academico_id_nino",
                table: "progreso_academico",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "IX_rel_docente_nino_materia_id_nino",
                table: "rel_docente_nino_materia",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nino_actividad_id_actividad",
                table: "rel_nino_actividad",
                column: "id_actividad");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nino_alergia_id_alergia",
                table: "rel_nino_alergia",
                column: "id_alergia");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nino_condicion_id_condicion",
                table: "rel_nino_condicion",
                column: "id_condicion");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nino_contacto_emergencia_id_contacto",
                table: "rel_nino_contacto_emergencia",
                column: "id_contacto");

            migrationBuilder.CreateIndex(
                name: "IX_rel_nino_medicamento_id_medicamento",
                table: "rel_nino_medicamento",
                column: "id_medicamento");

            migrationBuilder.CreateIndex(
                name: "idx_rel_padres_ninos_nino",
                table: "rel_padres_ninos",
                column: "id_nino");

            migrationBuilder.CreateIndex(
                name: "UQ__roles__72AFBCC692ECDCF1",
                table: "roles",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__tipo_act__717F08BCA36A2042",
                table: "tipo_actividad",
                column: "nombre_tipo_actividad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__tipo_pag__3E5147E208217D9B",
                table: "tipo_pagos",
                column: "nombre_tipo_pago",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_usuarios_rol",
                table: "usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asistencia");

            migrationBuilder.DropTable(
                name: "evaluaciones");

            migrationBuilder.DropTable(
                name: "observaciones_docentes");

            migrationBuilder.DropTable(
                name: "pagos");

            migrationBuilder.DropTable(
                name: "progreso_academico");

            migrationBuilder.DropTable(
                name: "rel_docente_nino_materia");

            migrationBuilder.DropTable(
                name: "rel_nino_actividad");

            migrationBuilder.DropTable(
                name: "rel_nino_alergia");

            migrationBuilder.DropTable(
                name: "rel_nino_condicion");

            migrationBuilder.DropTable(
                name: "rel_nino_contacto_emergencia");

            migrationBuilder.DropTable(
                name: "rel_nino_medicamento");

            migrationBuilder.DropTable(
                name: "rel_padres_ninos");

            migrationBuilder.DropTable(
                name: "tipo_pagos");

            migrationBuilder.DropTable(
                name: "docentes");

            migrationBuilder.DropTable(
                name: "actividades");

            migrationBuilder.DropTable(
                name: "alergias");

            migrationBuilder.DropTable(
                name: "condiciones_medicas");

            migrationBuilder.DropTable(
                name: "contactos_emergencia");

            migrationBuilder.DropTable(
                name: "medicamentos");

            migrationBuilder.DropTable(
                name: "ninos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "tipo_actividad");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
