using AutoMapper;
using TheGymProject.DTO;

namespace TheGymProject.Mappings
{
    public class AsistenciaProfile : Profile
    {
        public AsistenciaProfile()
        {
            CreateMap<Asistencia, AsistenciaDto>().ReverseMap();
            CreateMap<Alumno, AlumnoDto>().ReverseMap();
        }
    }
}
