using MediatR;

namespace WeShop.Domain.Events.Catalog
{
    public class TypeDeleteDomainEvent : INotification
    {
        public long TypeId { get; private set; }

        public TypeDeleteDomainEvent(long typeId)
        {
            TypeId = typeId;
        }
    }
}