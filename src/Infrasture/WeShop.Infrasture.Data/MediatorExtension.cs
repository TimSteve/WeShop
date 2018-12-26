using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WeShop.Domain.Abstract;

namespace WeShop.Infrasture.Data
{
    /// <summary>
    /// Mediator扩展: 1.取出所有实体中的领域事件，2.清除并分发领域事件
    /// </summary>
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, WeShopDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => { await mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
        }
    }
}