using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGymProject.Migrations
{
    public partial class AgregarDiasAdicionalesAAlumno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiasAdicionales",
                table: "Alumno",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasAdicionales",
                table: "Alumno");
        }
    }
}
