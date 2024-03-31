using MediatR;
using Problem.EFCore.Infrastructure;
using System.Text.Json;

namespace Problem.EFCore.Application
{
    internal class TodoToogleNotificationHandler : INotificationHandler<TodoToogleNotification>
    {
        private readonly IAzureStorageQueueService _azureStorageQueueService;

        public TodoToogleNotificationHandler(IAzureStorageQueueService azureStorageQueueService)
        {
            _azureStorageQueueService = azureStorageQueueService;
        }

        public async Task Handle(TodoToogleNotification notification, CancellationToken cancellationToken)
        {
            await _azureStorageQueueService.InsertMessageAsync("todoqueue", JsonSerializer.Serialize(notification));
        }
    }
}
