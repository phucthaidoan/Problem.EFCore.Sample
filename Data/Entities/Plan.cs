namespace Problem.EFCore.Sample.Data.Entities
{
    public class Plan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; }
        public ICollection<Todo> Todos { get; set; }
    }
}
