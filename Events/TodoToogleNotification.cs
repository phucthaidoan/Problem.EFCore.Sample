using MediatR;
using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data.Entities;
using Problem.EFCore.Sample.Data;

namespace Problem.EFCore.Sample.Events
{
    public class TodoToogleNotification : INotification
    {
        public Guid TodoId { get; set; }
        public bool ToogleValue { get; set; }
        public DateTime OccurredDate { get; set; }
    }

    public class TodoToogleNotificationHandler : INotificationHandler<TodoToogleNotification>
    {
        private readonly TodoDbContext _dbContext;

        public TodoToogleNotificationHandler(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Handle(TodoToogleNotification notification, CancellationToken cancellationToken)
        {
            var planData = await _dbContext
                .Plans
                .Where(plan => plan.Todos.Any(todo => todo.Id == notification.TodoId))
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
