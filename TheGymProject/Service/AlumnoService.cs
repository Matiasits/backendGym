using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.Service
{
    public class AlumnoService : IAlumnoService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;

        public AlumnoService(GimnasioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlumnoDto>> GetAlumnos()
        {
            var alumnos = await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                .Include(ap => ap.Plan)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlumnoDto>>(alumnos);
        }

        public async Task<bool> CreateAlumno(AlumnoDto alumnoDto)
        {
            var alumno = _mapper.Map<Alumno>(alumnoDto);
            _context.Alumno.Add(alumno);
            await _context.SaveChangesAsync();

            var alumnoPlan = new AlumnoPlan
            {
                AlumnoId = alumno.AlumnoId,
                PlanId = alumno.PlanId,
                FHInicio = DateTime.Now,
                FHVencimiento = DateTime.Now.AddMonths(1)
            };

            _context.AlumnoPlan.Add(alumnoPlan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAlumno(int dni, AlumnoDto alumnoDto)
        {
            var alumno = await _context.Alumno.FirstOrDefaultAsync(a => a.DNI == dni);
            if (alumno == null) return false;

            _mapper.Map(alumnoDto, alumno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlumno(int dni)
        {
            var alumno = await _context.Alumno.FirstOrDefaultAsync(a => a.DNI == dni);
            if (alumno == null) return false;

            _context.Alumno.Remove(alumno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AlumnoDto?> ObtenerAlumnoPorDni(int dni)
        {
            var alumno = await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                .ThenInclude(ap => ap.Plan)
                .FirstOrDefaultAsync(a => a.DNI == dni);

            return alumno is not null ? _mapper.Map<AlumnoDto>(alumno) : null;
        }

    }
}
