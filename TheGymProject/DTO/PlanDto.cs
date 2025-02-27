namespace TheGymProject.DTO
{
    public class PlanDto
    {
        public string Nombre { get; set; } = null!;
        public DateTime FHInicio { get; set; }
        public DateTime FHVencimiento { get; set; }
        public string FechaInicioFormateada => FHInicio.ToString("dd/MM/yyyy");
        public string FechaVencimientoFormateada => FHVencimiento.ToString("dd/MM/yyyy");
        public decimal Precio { get; set; }
    }
}
