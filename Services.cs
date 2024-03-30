using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data;
using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Sample
{
    public class CreatePlanRequest
    {
        public string Name { get; set; }
    }

    public class CreateTodoRequest
    {
        public string Description { get; set; }
    }

    public class ToogleTodoDoneRequest
    {
        public bool IsDone { get; set; }
    }

    public interface IPlanService
    {
        Task<IEnumerable<PlanWithTodoListDTO>> GetAllAsync();
        Task CreateAsync(CreatePlanRequest request);
        Task<TodoWithPlanDTO> GetByPlanIdAsync(Guid planId);
    }

    public interface ITodoService
    {
        Task CreateAsync(Guid planId, CreateTodoRequest request);

        Task ToogleDoneAsync(Guid todoId, ToogleTodoDoneRequest request);
    }

    public class PlanService : IPlanService
    {
        private readonly TodoDbContext _dbContext;

        public PlanService(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(CreatePlanRequest request)
        {
            await _dbContext
                            .AddAsync(new Plan
                            {
                                CreatedDate = DateTime.Now,
                                Name = request.Name,
                            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlanWithTodoListDTO>> GetAllAsync()
        {
            return await _dbContext
                .Plans
                .Select(plan => new PlanWithTodoListDTO
                {
                    Id = plan.Id,
                    Name = plan.Name,
                    CreatedDate = plan.CreatedDate,
                    UpdatedDate = plan.UpdatedDate,
                    CompletedDate = plan.CompletedDate,
                    Todos = plan.Todos.Select(todo => new TodoDTO
                    {
                        Id = todo.Id,
                        Description = todo.Description,
                        IsDone = todo.IsDone,
                        UpdatedDate = todo.UpdatedDate,
                        CompletedDate = todo.CompletedDate,
                        CreatedDate = todo.CreatedDate
                    })
                })
                .ToListAsync();
        }

        public async Task<TodoWithPlanDTO> GetByPlanIdAsync(Guid planId)
        {
            return await _dbContext
                            .Todos
                            .Where(todo => todo.PlanId == planId)
                            .Select(todo => new TodoWithPlanDTO
                            {
                                Id = todo.Id,
                                CompletedDate = todo.CompletedDate,
                                CreatedDate = todo.CreatedDate,
                                Description = todo.Description,
                                IsDone = todo.IsDone,
                                Plan = new PlanDTO
                                {
                                    CreatedDate = todo.Plan.CreatedDate,
                                    Id = todo.Plan.Id,
                                    Name = todo.Plan.Name,
                                    UpdatedDate = todo.Plan.UpdatedDate,
                                    CompletedDate = todo.Plan.CompletedDate
                                }
                            })
                            .FirstOrDefaultAsync();

        }
    }

    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _dbContext;

        public TodoService(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Guid planId, CreateTodoRequest request)
        {
            await _dbContext.Todos.AddAsync(new Todo
            {
                Description = request.Description,
                PlanId = planId,
                CreatedDate = DateTime.Now,
                IsDone = false
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task ToogleDoneAsync(Guid todoId, ToogleTodoDoneRequest request)
        {
            var todo = await _dbContext
                .Todos
                .Where(todo => todo.Id == todoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (todo is null)
            {
                throw new Exception("Todo not found");
            }

            todo.IsDone = request.IsDone;
            todo.UpdatedDate = DateTime.Now;
            _dbContext.Update(todo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
