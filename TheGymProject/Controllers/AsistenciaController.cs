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
            var (success, diasRestantes, planActivo, diasAdicionales, esPaseLire) = await _asistenciaService.RegistrarAsistenciaAlumno(asistenciaDto);

            if (!success) return NotFound("Alumno no encontrado.");

            return Ok(new
            {
                Message = "Asistencia registrada correctamente.",
                DiasRestantes = diasRestantes,
                PlanActivo = planActivo,
                DiasAdicionaes = diasAdicionales,
                esPaseLibre = esPaseLire
            });
        }

    }
}
