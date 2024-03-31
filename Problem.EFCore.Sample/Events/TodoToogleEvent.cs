using MediatR;
using Problem.EFCore.Infrastructure;
using System.Text.Json;

namespace Problem.EFCore.Sample.Events
{
    public class TodoToogleEvent : INotification
    {
        public Guid TodoId { get; set; }
        public bool ToogleValue { get; set; }
        public DateTime OccurredDate { get; set; }
    }

    public class TodoToogleEventHandler : INotificationHandler<TodoToogleEvent>
    {
        private readonly IAzureStorageQueueService _azureStorageQueueService;

        public TodoToogleEventHandler(IAzureStorageQueueService azureStorageQueueService)
        {
            _azureStorageQueueService = azureStorageQueueService;
        }

        public async Task Handle(TodoToogleEvent notification, CancellationToken cancellationToken)
        {
            await _azureStorageQueueService.InsertMessageAsync("todoqueue", JsonSerializer.Serialize(notification));
        }
    }
}
