using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Sample.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-IAJ1J0A2;Database=todo_01simple;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await HandleModifiedTodoEntitiesAsync();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task HandleModifiedTodoEntitiesAsync()
        {
            var modifiedTodoEntries = ChangeTracker
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

                    await CheckToCompletePlanAsync(todoEntity);
                }
            }
        }

        private async Task CheckToCompletePlanAsync(Todo todo)
        {
            
            var planData = await Plans
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

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Plan> Plans { get; set; }

    }
}
