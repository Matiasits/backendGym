using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IAsistenciaService
    {
        Task<(bool Success, int DiasRestantes, bool PlanActivo, int? DiasAdicionales, bool EsPlanTresVecesPorSemana, string FechaInicioFormateada, string FechaVencimientoFormateada)>
        RegistrarAsistenciaAlumno(AsistenciaDto asistenciaDto);

        Task<(List<AlumnoDto> Alumnos, int Cantidad, decimal GananciaTotal)> ObtenerResumenMensual();
    }
}
