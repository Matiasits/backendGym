using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IAlumnoService
    {
        Task<IEnumerable<AlumnoDto>> GetAlumnos();
        Task<bool> CreateAlumno(AlumnoDto alumnoDto);
        Task<bool> UpdateAlumno(int dni, AlumnoDto alumnoDto);
        Task<bool> DeleteAlumno(int dni);
        Task<AlumnoDto?> ObtenerAlumnoPorDni(int dni);
    }
}
