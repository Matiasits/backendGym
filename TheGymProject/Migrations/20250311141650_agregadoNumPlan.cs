using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGymProject.Migrations
{
    public partial class agregadoNumPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroPlan",
                table: "Alumno",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroPlan",
                table: "Alumno");
        }
    }
}
