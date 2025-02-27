using AutoMapper;
using TheGymProject.DTO;

public class AlumnoProfile : Profile
{
    public AlumnoProfile()
    {
        CreateMap<AlumnoDto, Alumno>()
            .ForMember(dest => dest.Plan, opt => opt.Ignore()) // Ignoramos para evitar conflictos en la asignación
            .ReverseMap()
            .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.Plan.PlanId)); // Aseguramos que PlanId se mapee correctamente
    }
}
