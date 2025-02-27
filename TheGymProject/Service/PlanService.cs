using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.InterfacesService
{
    public class PlanService : IPlanService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;

        public PlanService(GimnasioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanDto>> GetPlanes()
        {
            var planes = await _context.Planes.ToListAsync();
            return _mapper.Map<IEnumerable<PlanDto>>(planes);
        }

        public async Task<PlanDto?> GetPlanById(int id)
        {
            var plan = await _context.Planes.FindAsync(id);
            return plan == null ? null : _mapper.Map<PlanDto>(plan);
        }

        public async Task<bool> CreatePlan(PlanDto planDto)
        {
            var plan = _mapper.Map<Plan>(planDto);
            _context.Planes.Add(plan);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdatePlan(int id, PlanDto planDto)
        {
            var plan = await _context.Planes.FindAsync(id);
            if (plan == null) return false;

            _mapper.Map(planDto, plan);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeletePlan(int id)
        {
            var plan = await _context.Planes.FindAsync(id);
            if (plan == null) return false;

            _context.Planes.Remove(plan);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
