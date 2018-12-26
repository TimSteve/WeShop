using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;

namespace WeShop.Domain.Events.Catalog
{
    public class BrandDeleteDomainEventHandler : INotificationHandler<BrandDeleteDomainEvent>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ILogger _logger;

        public BrandDeleteDomainEventHandler(
            IRepository<Product> productRepo,
            ILoggerFactory loggerFactory)
        {
            _productRepo = productRepo;
            _logger = loggerFactory.CreateLogger<BrandDeleteDomainEventHandler>();
        }

        public Task Handle(BrandDeleteDomainEvent notification, CancellationToken cancellationToken)
        {
            var products = _productRepo.GetAll()
                .Where(x => x.ProductBrandId == notification.BrandId)
                .ToList();
            foreach (var product in products)
            {
                product.SetBrand(1);
                _productRepo.Update(product);
            }

            return Task.CompletedTask;
        }
    }
}