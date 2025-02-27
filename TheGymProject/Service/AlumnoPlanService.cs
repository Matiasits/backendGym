using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheGymProject.Service
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;

        public AlumnoPlanService(GimnasioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlumnoPlanDto>> GetAlumnoPlanes()
        {
            var alumnoPlanes = await _context.AlumnoPlan
                .Include(ap => ap.Alumno)
                .Include(ap => ap.Plan)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AlumnoPlanDto>>(alumnoPlanes);
        }

        public async Task<bool> CreateAlumnoPlan(AlumnoPlanDto alumnoPlanDto)
        {
            var alumnoPlan = _mapper.Map<AlumnoPlan>(alumnoPlanDto);
            _context.AlumnoPlan.Add(alumnoPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAlumnoPlan(int id, AlumnoPlanDto alumnoPlanDto)
        {
            var alumnoPlan = await _context.AlumnoPlan.FirstOrDefaultAsync(ap => ap.AlumnoPlanId == id);
            if (alumnoPlan == null) return false;

            _mapper.Map(alumnoPlanDto, alumnoPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlumnoPlan(int id)
        {
            var alumnoPlan = await _context.AlumnoPlan.FirstOrDefaultAsync(ap => ap.AlumnoPlanId == id);
            if (alumnoPlan == null) return false;

            _context.AlumnoPlan.Remove(alumnoPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RenovarSuscripcion(int dni, int diasARestar = 0)
        {
            var alumnoPlan = await _context.AlumnoPlan
                .Include(ap => ap.Alumno) // Unir con la tabla Alumno
                .FirstOrDefaultAsync(ap => ap.Alumno.DNI == dni);

            if (alumnoPlan == null)
            {
                return false; // No se encontró la suscripción del alumno
            }

            // Reiniciar a 0 los días adeudados o restar los días proporcionados
            alumnoPlan.Alumno.DiasAdicionales = Math.Max(0, alumnoPlan.Alumno.DiasAdicionales - diasARestar);

            // Renovar el plan con nuevas fechas
            alumnoPlan.FHInicio = DateTime.Now;
            alumnoPlan.FHVencimiento = DateTime.Now.AddMonths(1);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
