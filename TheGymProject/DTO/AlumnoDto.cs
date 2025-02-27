using System.ComponentModel.DataAnnotations;

namespace TheGymProject.DTO
{
    public class AlumnoDto
    {
        public int DNI { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Domicilio { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string TelefonoEmergencia { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroFormateada => FechaRegistro.ToString("dd/MM/yyyy");
        public int DiasAdicionales { get; set; }
        public int PlanId { get; set; }

        public List<AlumnoPlanDto> AlumnoPlanes { get; set; } = new List<AlumnoPlanDto>();

    }
}
