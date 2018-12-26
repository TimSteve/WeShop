using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WeShop.Domain.Abstract;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Data.EntityConfigurations;

namespace WeShop.Infrasture.Data
{
    public class WeShopDbContext : DbContext, IUnitOfWork
    {
        public WeShopDbContext(DbContextOptions<WeShopDbContext> options) : base(options)
        {
        }

        public WeShopDbContext(DbContextOptions<WeShopDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductTypeEntityTypeConfiguration());
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction =
                _currentTransaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // 在分发领域事件如果发生了异常，就不会执行 SaveChangesAsync，数据将不会被提交
            await _mediator.DispatchDomainEventsAsync(this);

            var changeNum = await SaveChangesAsync(cancellationToken);
            return changeNum > 0;
        }
    }
}