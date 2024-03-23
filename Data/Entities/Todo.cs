namespace Problem.EFCore.Sample.Data.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        public Guid PlanId { get; set; }
        public Plan Plan { get; set; }
    }
}
