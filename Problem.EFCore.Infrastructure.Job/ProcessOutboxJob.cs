using MediatR;
using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Application;
using Problem.EFCore.Infrastructure.Data;
using Quartz;
using System.Text.Json;

namespace Problem.EFCore.Infrastructure.Job
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly TodoDbContext _todoDbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxJob(TodoDbContext todoDbContext, IPublisher publisher)
        {
            _todoDbContext = todoDbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var notificationList = await _todoDbContext
                .OutboxMessages
                .Where(message => !message.ProcessedDate.HasValue)
                .ToListAsync();

            foreach (var notification in notificationList)
            {
                var todoToogleNotification = JsonSerializer.Deserialize(notification.Data, typeof(TodoToogleNotification));
                if (todoToogleNotification is null)
                {
                    continue;
                }
                
                await _publisher.Publish(todoToogleNotification);

                notification.ProcessedDate = DateTime.UtcNow;
            }

            await _todoDbContext.SaveChangesAsync();
        }
    }
}
