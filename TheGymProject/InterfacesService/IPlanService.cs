using TheGymProject.DTO;

namespace TheGymProject.InterfacesService
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDto>> GetPlanes();
        Task<PlanDto?> GetPlanById(int id);
        Task<bool> CreatePlan(PlanDto planDto);
        Task<bool> UpdatePlan(int id, PlanDto planDto);
        Task<bool> DeletePlan(int id);
    }
}
