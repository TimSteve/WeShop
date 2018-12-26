using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeShop.Domain.Abstract;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;
using WeShop.Domain.Entities;
using WeShop.Domain.Events.Catalog;
using WeShop.Domain.Services;
using WeShop.Infrasture.Data;
using WeShop.Infrasture.Repositories;
using WeShop.Queries;

namespace WeShop.Bootstrapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<WeShopDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("Sqlite")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IRepository<ProductBrand>, BaseRepository<ProductBrand>>();
            services.AddScoped<IRepository<ProductType>, BaseRepository<ProductType>>();

            return services;
        }

        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddScoped<IProductQueries, ProductQueries>();
            services.AddMediatR(typeof(CreateBrandCommandHandler).Assembly, typeof(BrandDeleteDomainEvent).Assembly,
                typeof(BrandDeleteDomainEventHandler).Assembly);
            services.AddScoped<IProductDomainService, ProductDomainService>();
            return services;
        }
    }
}