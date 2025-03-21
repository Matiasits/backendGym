namespace TheGymProject.DTO
{
    public class AlumnoPlanDto
    {
        public int AlumnoPlanId { get; set; }
        public int AlumnoId { get; set; }
        public int PlanId { get; set; }
        public DateTime FHInicio { get; set; } = DateTime.Now;
        public DateTime FHVencimiento { get; set; }
        public string FechaInicioFormateada => FHInicio.ToString("dd/MM/yyyy");
        public string FechaVencimientoFormateada => FHVencimiento.ToString("dd/MM/yyyy");
        // Relaciones
        public AlumnoDto Alumno { get; set; } = null!;
        public PlanDto Plan { get; set; } = null!;
    }
}
