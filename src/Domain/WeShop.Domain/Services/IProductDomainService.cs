using System.Threading.Tasks;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Services
{
    public interface IProductDomainService
    {
        Task<Result> PreInsertAsync(Product product);

        Task<Result> PreUpdateAsync(Product product);
    }
}