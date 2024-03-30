using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using Problem.EFCore.Infrastructure.Options;
using System.Text;

namespace Problem.EFCore.Infrastructure
{
    public interface IAzureStorageQueueService
    {
        Task InsertMessageAsync(string queueName, string newMessage);
    }

    public class AzureStorageQueueService : IAzureStorageQueueService
    {
        private readonly AzureStorageOption _azureStorageOption;

        public AzureStorageQueueService(IOptions<AzureStorageOption> options)
        {
            _azureStorageOption = options.Value;
        }

        public async Task InsertMessageAsync(string queueName, string newMessage)
        {
            QueueClient queue = new QueueClient(_azureStorageOption.ConnectionString, queueName);

            await queue.CreateIfNotExistsAsync();
            var bytes = Encoding.UTF8.GetBytes(newMessage);

            await queue.SendMessageAsync(Convert.ToBase64String(bytes), default, TimeSpan.FromSeconds(-1), default);
        }
    }
}
