namespace Problem.EFCore.Sample
{
    public class CheckToCompletePlanModel
    {
        public Guid PlanId { get; set; }
        public bool IsTaskDone { get; set; }
        public Guid TaskToToogleDoneId { get; set; }
    }
}
