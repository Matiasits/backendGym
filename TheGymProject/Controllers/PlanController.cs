using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanesController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlanes()
        {
            var planes = await _planService.GetPlanes();
            return Ok(planes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetPlanById(int id)
        {
            var plan = await _planService.GetPlanById(id);
            if (plan == null) return NotFound("Plan no encontrado.");
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(PlanDto planDto)
        {
            var success = await _planService.CreatePlan(planDto);
            if (!success) return BadRequest("No se pudo crear el plan.");
            return Ok("Plan creado correctamente.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, PlanDto planDto)
        {
            var success = await _planService.UpdatePlan(id, planDto);
            if (!success) return NotFound("Plan no encontrado.");
            return Ok("Plan actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var success = await _planService.DeletePlan(id);
            if (!success) return NotFound("Plan no encontrado.");
            return Ok("Plan eliminado correctamente.");
        }
    }
}
