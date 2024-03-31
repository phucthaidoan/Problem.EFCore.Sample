namespace Problem.EFCore.Infrastructure.Data.Entities
{
    public class OutboxMessage
    {
        /// <summary>
        /// Id of message.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Occurred on.
        /// </summary>
        public DateTime OccurredOn { get; set; }

        /// <summary>
        /// Full name of message type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Message data - serialzed to JSON.
        /// </summary>
        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }
        
    }
}
