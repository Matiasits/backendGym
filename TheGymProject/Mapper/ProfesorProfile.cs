using AutoMapper;
using TheGymProject.DTO;

namespace TheGymProject.Mappings
{
    public class ProfesorProfile : Profile
    {
        public ProfesorProfile()
        {
            CreateMap<Profesor, ProfesorDto>().ReverseMap();
        }
    }
}
