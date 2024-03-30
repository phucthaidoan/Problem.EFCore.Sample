using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TotoFunctionApp
{
    public class TodoFunction
    {
        private readonly ILogger<TodoFunction> _logger;
        private readonly IPlanService _planService;

        public TodoFunction(ILogger<TodoFunction> logger, IPlanService planService)
        {
            _logger = logger;
            _planService = planService;
        }

        [Function(nameof(TodoFunction))]
        public async Task Run([QueueTrigger(
            queueName: "todoqueue", 
            Connection = "Function:AzureStorage:ConnectionString")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            var todoToogleEvent = JsonSerializer.Deserialize<TodoToogleEvent>(message.MessageText);
            await _planService.HandleTodoToogleAsync(todoToogleEvent);
        }
    }
}
