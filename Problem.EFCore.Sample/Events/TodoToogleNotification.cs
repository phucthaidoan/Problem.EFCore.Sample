using MediatR;
using Problem.EFCore.Infrastructure;
using Problem.EFCore.Infrastructure.Data;
using Problem.EFCore.Sample.Data;
using System.Text.Json;

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
        private readonly IAzureStorageQueueService _azureStorageQueueService;

        public TodoToogleNotificationHandler(TodoDbContext dbContext, IAzureStorageQueueService azureStorageQueueService)
        {
            _azureStorageQueueService = azureStorageQueueService;
        }


        public async Task Handle(TodoToogleNotification notification, CancellationToken cancellationToken)
        {
            await _azureStorageQueueService.InsertMessageAsync("todoqueue", JsonSerializer.Serialize(notification));
        }
    }
}
