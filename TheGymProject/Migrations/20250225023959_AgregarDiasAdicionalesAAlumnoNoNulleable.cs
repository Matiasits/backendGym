using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGymProject.Migrations
{
    public partial class AgregarDiasAdicionalesAAlumnoNoNulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiasAdicionales",
                table: "Alumno",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiasAdicionales",
                table: "Alumno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
