using Problem.EFCore.Core.Application;
using System.Text.Json.Serialization;
namespace Problem.EFCore.Application
{
    public class TodoToogleNotification : DomainEventNotification
    {
        [JsonConstructor]
        public TodoToogleNotification(Guid id) : base(id)
        {
        }
    }
}
