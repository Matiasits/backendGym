using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IAlumnoPlanService
    {
        Task<bool> DeleteAlumnoPlan(int id);
        Task<bool> UpdateAlumnoPlan(int id, AlumnoPlanDto alumnoPlanDto);
        Task<bool> CreateAlumnoPlan(AlumnoPlanDto alumnoPlanDto);
        Task<IEnumerable<AlumnoPlanDto>> GetAlumnoPlanes();
        Task<bool> RenovarSuscripcion(int dni, int diasARestar);
        Task<bool> ActualizarPlanActivo(int dni, int nuevoPlanId);
    }
}
