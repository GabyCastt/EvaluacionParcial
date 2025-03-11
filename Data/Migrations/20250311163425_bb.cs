using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaEva.Data.Migrations
{
    /// <inheritdoc />
    public partial class bb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    autor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    nacionalidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.autor_id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    libro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    fecha_publicacion = table.Column<DateOnly>(type: "date", nullable: true),
                    isbn = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    autor_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.libro_id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores",
                        column: x => x.autor_id,
                        principalTable: "Autores",
                        principalColumn: "autor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_autor_id",
                table: "Libros",
                column: "autor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
