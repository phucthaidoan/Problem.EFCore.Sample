using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Sample
{
    public record PlanWithTodoListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IEnumerable<TodoDTO> Todos { get; set; }
    }

    public record TodoDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime? CompletedDate { get; set; }
    }

    public record PlanDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public record TodoWithPlanDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime? CompletedDate { get; set; }
        public PlanDTO Plan { get; set; }
    }
}
