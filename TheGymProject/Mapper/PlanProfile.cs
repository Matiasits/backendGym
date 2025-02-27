using AutoMapper;
using TheGymProject.DTO;

namespace TheGymProject.Mappings
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan, PlanDto>().ReverseMap();
        }
    }
}
