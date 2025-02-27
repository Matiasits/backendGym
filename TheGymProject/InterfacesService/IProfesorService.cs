using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorDto>> GetProfesores();
        Task<Profesor> CreateProfesor(ProfesorDto profesorDto);
        Task<bool> UpdateProfesor(int id, ProfesorDto profesorDto);
        Task<bool> DeleteProfesor(int id);
    }
}
