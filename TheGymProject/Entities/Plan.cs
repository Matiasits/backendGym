public class Plan
{
    public int PlanId { get; set; }
    public string Nombre { get; set; } = null!;
    public DateTime FHInicio { get; set; }
    public DateTime FHVencimiento { get; set; }
    public decimal Precio { get; set; }

    public ICollection<AlumnoPlan> AlumnoPlanes { get; set; } = new List<AlumnoPlan>();

}
