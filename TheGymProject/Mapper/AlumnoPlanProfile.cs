using AutoMapper;
using TheGymProject.DTO;

public class AlumnoPlanProfile : Profile
{
    public AlumnoPlanProfile()
    {
        CreateMap<AlumnoPlan, AlumnoPlanDto>()
            .ForMember(dest => dest.FechaInicioFormateada, opt => opt.MapFrom(src => src.FHInicio))
            .ForMember(dest => dest.FechaVencimientoFormateada, opt => opt.MapFrom(src => src.FHVencimiento))
            .ForMember(dest => dest.Alumno, opt => opt.MapFrom(src => src.Alumno))
            .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.Plan));

        CreateMap<AlumnoPlanDto, AlumnoPlan>();
    }
}
