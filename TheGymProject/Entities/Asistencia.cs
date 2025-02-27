using System;

public class Asistencia
{
    public int AsistenciaId { get; set; }
    public int AlumnoId { get; set; } 
    public Alumno Alumno { get; set; } = null!;
    public DateTime FHRegistro { get; set; } = DateTime.Now;
}
