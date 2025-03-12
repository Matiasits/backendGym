using System;
using System.Collections.Generic;

public class Alumno
{
    public int AlumnoId { get; set; }
    public int DNI { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? TelefonoEmergencia { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public int DiasAdicionales { get; set; }
    public int? NumeroPlan { get; set; } 
    public int PlanId { get; set; }

    // Relaciones
    public ICollection<AlumnoPlan> AlumnoPlanes { get; set; } = new List<AlumnoPlan>();
    public Plan Plan { get; set; } = null!;
    public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
}
