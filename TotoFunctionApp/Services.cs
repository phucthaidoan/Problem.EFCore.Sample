using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Infrastructure.Data;
using TotoFunctionApp.Events;

namespace TotoFunctionApp
{
    public interface IPlanService
    {
        Task HandleTodoToogleAsync(TodoToogleEvent @event);
    }

    public class PlanService : IPlanService
    {
        private readonly TodoDbContext _dbContext;

        public PlanService(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleTodoToogleAsync(TodoToogleEvent @event)
        {
            var planData = await _dbContext
                .Plans
                .Where(plan => plan.Todos.Any(todo => todo.Id == @event.TodoId))
                .Select(plan => new
                {
                    Plan = plan,
                    HasTodoItemNotDone = plan
                        .Todos
                        .Any(todo => !todo.IsDone)
                })
                .FirstOrDefaultAsync();

            if (planData is null)
            {
                return;
            }

            if (planData.HasTodoItemNotDone)
            {
                planData.Plan.UpdatedDate = DateTime.Now;
                planData.Plan.CompletedDate = null;
            }
            else
            {
                planData.Plan.UpdatedDate = DateTime.Now;
                planData.Plan.CompletedDate = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
