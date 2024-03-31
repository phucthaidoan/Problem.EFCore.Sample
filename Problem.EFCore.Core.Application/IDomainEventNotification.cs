using MediatR;

namespace Problem.EFCore.Core.Application
{
    public abstract class DomainEventNotification : INotification
    {
        public Guid Id { get; set; }

        protected DomainEventNotification(Guid id)
        {
            Id = id;
        }
    }
}
