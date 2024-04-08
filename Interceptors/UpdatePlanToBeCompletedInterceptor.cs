using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Problem.EFCore.Sample.Data.Entities;
using Problem.EFCore.Sample.Data;

namespace Problem.EFCore.Sample.Interceptors
{
    public class UpdatePlanToBeCompletedInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var todoDbContext = eventData.Context as TodoDbContext;
            if (todoDbContext is not null)
            {
                // TODO if multiple todo entities are from the same plan => let's resolve it!
                await HandleModifiedTodoEntitiesAsync(todoDbContext);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task HandleModifiedTodoEntitiesAsync(TodoDbContext todoDbContext)
        {
            var modifiedTodoEntries = todoDbContext
                .ChangeTracker
                .Entries<Todo>()
                .Where(todoEntry => todoEntry.State == EntityState.Modified)
                .ToList();

            foreach (var entry in modifiedTodoEntries)
            {
                var todoEntity = entry.Entity;
                var isOriginalTaskDone = false;
                entry.OriginalValues.TryGetValue(nameof(Todo.IsDone), out isOriginalTaskDone);

                var isCurrentTaskDone = false;
                entry.CurrentValues.TryGetValue(nameof(Todo.IsDone), out isCurrentTaskDone);

                if (isOriginalTaskDone != isCurrentTaskDone)
                {
                    todoEntity.CompletedDate = isCurrentTaskDone ? DateTime.Now : null;

                    await CheckToCompletePlanAsync(todoDbContext, todoEntity);
                }
            }
        }

        private async Task CheckToCompletePlanAsync(TodoDbContext todoDbContext, Todo todo)
        {

            var planData = await todoDbContext
                .Plans
                .Where(plan => plan.Id == todo.PlanId)
                .Select(plan => new
                {
                    Plan = plan,
                    HasTodoItemNotDone = plan
                        .Todos
                        .Where(todo => todo.Id != todo.Id)
                        .Any(todo => !todo.IsDone)
                })
                .FirstOrDefaultAsync();

            if (planData is null)
            {
                return;
            }

            var isAllItemsDone = todo.IsDone && !planData.HasTodoItemNotDone;
            if (!isAllItemsDone)
            {
                planData.Plan.UpdatedDate = DateTime.Now;
                planData.Plan.CompletedDate = null;
            }
            else
            {
                planData.Plan.UpdatedDate = DateTime.Now;
                planData.Plan.CompletedDate = DateTime.Now;
            }
        }

    }
}
