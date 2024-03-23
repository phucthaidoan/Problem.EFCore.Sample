using Microsoft.AspNetCore.Mvc;

namespace Problem.EFCore.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPut("{todoId}/done")]
        public async Task ToogleDone(Guid todoId, ToogleTodoDoneRequest request)
        {
            await _todoService.ToogleDoneAsync(todoId, request);
        }
    }
}
