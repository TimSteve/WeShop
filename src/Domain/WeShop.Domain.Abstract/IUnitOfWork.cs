using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeShop.Domain.Abstract
{
    /// <summary>
    /// 工作单元模式
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}