using Microsoft.AspNetCore.Mvc;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;
using TheGymProject.Service;

namespace TheGymProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoDto>>> GetAlumnos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var alumnos = await _alumnoService.GetAlumnos(page, pageSize);
            return Ok(alumnos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlumno(AlumnoDto alumnoDto)
        {
            var success = await _alumnoService.CreateAlumno(alumnoDto);
            if (!success) return BadRequest("No se pudo crear el alumno.");
            return Ok("Alumno creado correctamente.");
        }

        [HttpPut("{dni}")]
        public async Task<IActionResult> UpdateAlumno(int dni, AlumnoDto alumnoDto)
        {
            var success = await _alumnoService.UpdateAlumno(dni, alumnoDto);
            if (!success) return NotFound("Alumno no encontrado.");
            return Ok("Alumno actualizado correctamente.");
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> DeleteAlumno(int dni)
        {
            var success = await _alumnoService.DeleteAlumno(dni);
            if (!success) return NotFound("Alumno no encontrado.");
            return Ok("Alumno eliminado correctamente.");
        }


        [HttpGet("{dni}")]
        public async Task<IActionResult> ObtenerAlumnoPorDni(int dni)
        {
            var alumno = await _alumnoService.ObtenerAlumnoPorDni(dni);
            if (alumno == null)
            {
                return NotFound(new { mensaje = "Alumno no encontrado" });
            }
            return Ok(alumno);
        }
        
    }
}
