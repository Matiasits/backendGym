using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IAlumnoService
    {
        Task<IEnumerable<AlumnoDto>> GetAlumnos(int page = 1, int pageSize = 10);
        Task<bool> CreateAlumno(AlumnoDto alumnoDto);
        Task<bool> UpdateAlumno(int dni, AlumnoDto alumnoDto);
        Task<bool> DeleteAlumno(int dni);
        Task<AlumnoDto?> ObtenerAlumnoPorDni(int dni);
    }
}
