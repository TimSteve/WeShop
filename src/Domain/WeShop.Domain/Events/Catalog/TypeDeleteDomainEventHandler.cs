using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;

namespace WeShop.Domain.Events.Catalog
{
    public class TypeDeleteDomainEventHandler : INotificationHandler<TypeDeleteDomainEvent>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ILogger _logger;

        public TypeDeleteDomainEventHandler(
            IRepository<Product> productRepo,
            ILoggerFactory loggerFactory)
        {
            _productRepo = productRepo;
            _logger = loggerFactory.CreateLogger<TypeDeleteDomainEventHandler>();
        }

        public Task Handle(TypeDeleteDomainEvent notification, CancellationToken cancellationToken)
        {
            var products = _productRepo.GetAll()
                .Where(x => x.ProductTypeId == notification.TypeId)
                .ToList();
            foreach (var product in products)
            {
                product.SetType(1);
                _productRepo.Update(product);
            }

            return Task.CompletedTask;
        }
    }
}