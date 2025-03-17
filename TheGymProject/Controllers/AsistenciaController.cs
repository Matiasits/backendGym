using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;
using TheGymProject.Service;

namespace TheGymProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciasController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarAsistencia([FromBody] AsistenciaDto asistenciaDto)
        {
            var (success, diasRestantes, planActivo, diasAdicionales, esPlanTresVecesPorSemana, fechaInicio, fechaVencimiento) =
                await _asistenciaService.RegistrarAsistenciaAlumno(asistenciaDto);

            if (!success) return NotFound("Alumno no encontrado.");

            return Ok(new
            {
                Message = "Asistencia registrada correctamente.",
                DiasRestantes = diasRestantes,
                PlanActivo = planActivo,
                DiasAdicionales = diasAdicionales,
                EsPlanTresVecesPorSemana = esPlanTresVecesPorSemana,
                FechaInicioFormateada = fechaInicio,
                FechaVencimientoFormateada = fechaVencimiento
            });
        }

        [HttpGet("resumen-mensual")]
        public async Task<IActionResult> ObtenerResumenMensual()
        {
            var (alumnos, cantidad, gananciaTotal) = await _asistenciaService.ObtenerResumenMensual();

            return Ok(new
            {
                Cantidad = cantidad,
                GananciaTotal = gananciaTotal,
                Alumnos = alumnos
            });
        }
    }
}
