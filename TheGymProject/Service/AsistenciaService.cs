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

        public async Task<(bool Success, int DiasRestantes, bool PlanActivo, int? DiasAdicionales, bool EsPlanTresVecesPorSemana, string FechaInicioFormateada, string FechaVencimientoFormateada)>
        RegistrarAsistenciaAlumno(AsistenciaDto asistenciaDto)
        {
            var alumno = await ObtenerAlumnoConPlanesYAsistencias(asistenciaDto.DNIAlumno);

            var asistencia = CrearAsistencia(asistenciaDto, alumno.AlumnoId);
            _context.Asistencia.Add(asistencia);

            var planActivo = ObtenerPlanActivo(alumno);
            var (diasRestantes, estaActivo, esPlanTresVecesPorSemana) = EvaluarEstadoDelPlan(planActivo, asistencia.FHRegistro);

            if (!estaActivo || esPlanTresVecesPorSemana)
            {
                ActualizarDiasAdicionales(alumno, esPlanTresVecesPorSemana);
            }

            await _context.SaveChangesAsync();

            var (fechaInicioFormateada, fechaVencimientoFormateada) = FormatearFechasPlan(planActivo);
            return (true, diasRestantes, estaActivo, alumno.DiasAdicionales, esPlanTresVecesPorSemana, fechaInicioFormateada, fechaVencimientoFormateada);
        }

        private async Task<Alumno> ObtenerAlumnoConPlanesYAsistencias(int dni)
        {
            return await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                    .ThenInclude(ap => ap.Plan)
                .Include(a => a.Asistencias)
                .FirstOrDefaultAsync(a => a.DNI == dni);
        }

        private Asistencia CrearAsistencia(AsistenciaDto asistenciaDto, int alumnoId)
        {
            var asistencia = _mapper.Map<Asistencia>(asistenciaDto);
            asistencia.AlumnoId = alumnoId;
            asistencia.FHRegistro = DateTime.Now;
            return asistencia;
        }

        private AlumnoPlan ObtenerPlanActivo(Alumno alumno)
        {
            return alumno.AlumnoPlanes
                .OrderByDescending(p => p.FHVencimiento)
                .FirstOrDefault();
        }

        private (int diasRestantes, bool estaActivo, bool esPlanTresVecesPorSemana) EvaluarEstadoDelPlan(AlumnoPlan planActivo, DateTime fechaRegistro)
        {
            if (planActivo == null) return (0, false, false);

            int diasRestantes = (planActivo.FHVencimiento - fechaRegistro).Days;
            bool estaActivo = fechaRegistro >= planActivo.FHInicio && fechaRegistro <= planActivo.FHVencimiento;
            bool esPlanTresVecesPorSemana = planActivo.PlanId == 3;

            return (diasRestantes, estaActivo, esPlanTresVecesPorSemana);
        }

        private void ActualizarDiasAdicionales(Alumno alumno, bool esPlanTresVecesPorSemana)
        {
            if (esPlanTresVecesPorSemana)
            {
                if (ValidarLimiteAsistenciasPlanSemanal(alumno))
                {
                    alumno.DiasAdicionales++;
                }
            }
            else
            {
                alumno.DiasAdicionales++;
            }
        }

        private bool ValidarLimiteAsistenciasPlanSemanal(Alumno alumno)
        {
            var hoy = DateTime.Now.Date;

            // Lunes de esta semana
            var inicioSemana = hoy.AddDays(-(int)hoy.DayOfWeek + (int)DayOfWeek.Monday);

            // Sábado de esta semana
            var finSemana = inicioSemana.AddDays(5);

            int asistenciasSemana = alumno.Asistencias
                .Where(a => a.FHRegistro.Date >= inicioSemana && a.FHRegistro.Date <= finSemana)
                .Count();

            return asistenciasSemana >= 3;
        }


        private (string fechaInicio, string fechaVencimiento) FormatearFechasPlan(AlumnoPlan planActivo)
        {
            if (planActivo == null) return (string.Empty, string.Empty);

            string fechaInicioFormateada = planActivo.FHInicio.ToString("dd/MM/yyyy");
            string fechaVencimientoFormateada = planActivo.FHVencimiento.ToString("dd/MM/yyyy");

            return (fechaInicioFormateada, fechaVencimientoFormateada);
        }
    }
}
