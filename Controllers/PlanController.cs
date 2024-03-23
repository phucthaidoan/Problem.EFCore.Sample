using Microsoft.AspNetCore.Mvc;

namespace Problem.EFCore.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planServices;
        private readonly ITodoService _todoService;

        public PlanController(IPlanService planServices, ITodoService todoService)
        {
            _planServices = planServices;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlanWithTodoListDTO>> GetAll()
        {
            return await _planServices.GetAllAsync();
        }

        [HttpPost]
        public async Task Create(CreatePlanRequest request)
        {
            await _planServices.CreateAsync(request);
        }

        [HttpGet("{planId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid planId)
        {
            var plan = await _planServices.GetByPlanIdAsync(planId);
            return Ok(plan);
        }

        [HttpPost("{planId}/todo")]
        public async Task Create([FromRoute] Guid planId, CreateTodoRequest request)
        {
            await _todoService.CreateAsync(planId, request);
        }
    }
}
