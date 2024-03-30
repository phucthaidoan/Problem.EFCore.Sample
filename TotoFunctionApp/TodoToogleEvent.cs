namespace TotoFunctionApp
{
    public class TodoToogleEvent
    {
        public Guid TodoId { get; set; }
        public bool ToogleValue { get; set; }
        public DateTime OccurredDate { get; set; }
    }
}
