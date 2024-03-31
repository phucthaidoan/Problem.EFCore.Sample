using MediatR;

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
        private readonly ILogger<TodoToogleEventHandler> _logger;
        public TodoToogleEventHandler(ILogger<TodoToogleEventHandler> logger)
        {
            _logger = logger;
        }

        public  Task Handle(TodoToogleEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Todo toogle event handler executing");
            return Task.CompletedTask;
        }
    }
}
