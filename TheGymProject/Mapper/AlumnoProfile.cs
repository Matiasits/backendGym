using AutoMapper;
using TheGymProject.DTO;

public class AlumnoProfile : Profile
{
    public AlumnoProfile()
    {
        CreateMap<AlumnoDto, Alumno>()
            .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId))
            .ReverseMap();
    }
}

