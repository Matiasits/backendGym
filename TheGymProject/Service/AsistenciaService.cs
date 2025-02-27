using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.Service
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;

        public AsistenciaService(GimnasioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool Success, int DiasRestantes, bool PlanActivo, int? DiasAdicionales, bool EsPlanTresVecesPorSemana)>RegistrarAsistenciaAlumno(AsistenciaDto asistenciaDto)
        {
            var alumno = await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                    .ThenInclude(ap => ap.Plan) // Incluir la relación con el Plan
                .FirstOrDefaultAsync(a => a.DNI == asistenciaDto.DNIAlumno);

            if (alumno == null) return (false, 0, false, 0, false);

            var asistencia = _mapper.Map<Asistencia>(asistenciaDto);
            asistencia.AlumnoId = alumno.AlumnoId;
            asistencia.FHRegistro = DateTime.Now;

            _context.Asistencia.Add(asistencia);

            // Obtener el plan más reciente del alumno
            var planActivo = alumno.AlumnoPlanes
                .OrderByDescending(p => p.FHVencimiento)
                .FirstOrDefault();

            int diasRestantes = 0;
            bool estaActivo = false;
            bool esPlanTresVecesPorSemana = false;

            if (planActivo != null)
            {
                diasRestantes = (planActivo.FHVencimiento - asistencia.FHRegistro).Days;
                estaActivo = asistencia.FHRegistro >= planActivo.FHInicio && asistencia.FHRegistro <= planActivo.FHVencimiento;

                // Verificar si el plan es de tres veces por semana (ajusta la condición según tu modelo)
                esPlanTresVecesPorSemana = planActivo.Plan.Nombre.Contains("Pase estandar", StringComparison.OrdinalIgnoreCase);

                if (!estaActivo)
                {
                    // Si el plan está vencido, incrementar el contador de días adicionales
                    alumno.DiasAdicionales++;
                }
            }
            else
            {
                // Si no tiene ningún plan, también contar los días adicionales
                alumno.DiasAdicionales++;
            }

            await _context.SaveChangesAsync();

            return (true, diasRestantes, estaActivo, alumno.DiasAdicionales, esPlanTresVecesPorSemana);
        }

    }
}
