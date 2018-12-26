using MediatR;

namespace WeShop.Domain.Events.Catalog
{
    public class BrandDeleteDomainEvent : INotification
    {
        public long BrandId { get; private set; }

        public BrandDeleteDomainEvent(long brandId)
        {
            BrandId = brandId;
        }
    }
}