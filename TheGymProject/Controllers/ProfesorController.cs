using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        private readonly IMapper _mapper;

        public ProfesoresController(IProfesorService profesorService, IMapper mapper)
        {
            _profesorService = profesorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetProfesores()
        {
            return Ok(await _profesorService.GetProfesores());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfesor(ProfesorDto profesorDto)
        {
            var profesor = await _profesorService.CreateProfesor(profesorDto);
            return CreatedAtAction(nameof(CreateProfesor), new { id = profesor.ProfesorId }, new { profesor.Nombre, profesor.Apellido, profesor.Email });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesor(int id, ProfesorDto profesorDto)
        {
            var updated = await _profesorService.UpdateProfesor(id, profesorDto);
            if (!updated) return NotFound("Profesor no encontrado.");

            return Ok("Profesor actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var deleted = await _profesorService.DeleteProfesor(id);
            if (!deleted) return NotFound("Profesor no encontrado.");

            return Ok("Profesor eliminado correctamente.");
        }
    }
}
