using Microsoft.AspNetCore.Mvc;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;


namespace TheGymProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoPlanController : ControllerBase
    {
        private readonly IAlumnoPlanService _alumnoPlanService;

        public AlumnoPlanController(IAlumnoPlanService alumnoPlanService)
        {
            _alumnoPlanService = alumnoPlanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoPlanDto>>> GetAlumnoPlanes()
        {
            var alumnoPlanes = await _alumnoPlanService.GetAlumnoPlanes();
            return Ok(alumnoPlanes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlumnoPlan(AlumnoPlanDto alumnoPlanDto)
        {
            var success = await _alumnoPlanService.CreateAlumnoPlan(alumnoPlanDto);
            if (!success) return BadRequest("No se pudo asignar el plan al alumno.");
            return Ok("Plan asignado correctamente.");
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAlumnoPlan(int id, AlumnoPlanDto alumnoPlanDto)
        //{
        //    var success = await _alumnoPlanService.UpdateAlumnoPlan(id, alumnoPlanDto);
        //    if (!success) return NotFound("Plan del alumno no encontrado.");
        //    return Ok("Plan del alumno actualizado correctamente.");
        //}

        [HttpDelete("borrarAlumno/{id}")]
        public async Task<IActionResult> DeleteAlumnoPlan(int id)
        {
            var success = await _alumnoPlanService.DeleteAlumnoPlan(id);
            if (!success) return NotFound("Plan del alumno no encontrado.");
            return Ok("Plan del alumno eliminado correctamente.");
        }

        [HttpPut("renovar/{dni}/{diasARestar}")]
        public async Task<IActionResult> RenovarSuscripcion(int dni, int diasARestar = 0)
        {
            var resultado = await _alumnoPlanService.RenovarSuscripcion(dni, diasARestar);

            if (!resultado)
                return BadRequest("No se pudo renovar la suscripción. Verifica el DNI del alumno.");

            return Ok("Suscripción renovada correctamente.");
        }
    }
}
