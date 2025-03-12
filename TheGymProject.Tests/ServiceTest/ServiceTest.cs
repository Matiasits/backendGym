using System;
using System.Collections.Generic;
using Xunit;
using TheGymProject.Service;

public class AsistenciaServiceTests
{
    private readonly AsistenciaService _service;

    public AsistenciaServiceTests()
    {
        _service = new AsistenciaService(null, null); // nulls si no necesitás dependencias
    }

    private Alumno CrearAlumnoConAsistencias(List<DateTime> fechasAsistencias)
    {
        return new Alumno
        {
            Asistencias = fechasAsistencias.ConvertAll(f => new Asistencia { FHRegistro = f })
        };
    }

    [Fact]
    public void Asistencias_SemanaActual_Cero()
    {
        var alumno = CrearAlumnoConAsistencias(new List<DateTime>());
        var resultado = LlamarMetodoValidar(alumno);
        Assert.False(resultado);
    }

    // ... demás tests igual que antes ...

    private bool LlamarMetodoValidar(Alumno alumno)
    {
        var method = typeof(AsistenciaService).GetMethod("ValidarLimiteAsistenciasPlanSemanal",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        return (bool)method.Invoke(_service, new object[] { alumno });
    }

    private DateTime ObtenerInicioSemana()
    {
        var hoy = DateTime.Now.Date;
        return hoy.AddDays(-(int)hoy.DayOfWeek + (int)DayOfWeek.Monday);
    }

    private DateTime HoyLunes() => ObtenerInicioSemana();
    private DateTime HoyMartes() => ObtenerInicioSemana().AddDays(1);
    private DateTime HoyMiercoles() => ObtenerInicioSemana().AddDays(2);
    private DateTime HoyJueves() => ObtenerInicioSemana().AddDays(3);
}
