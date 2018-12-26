using System.Collections.Generic;
using System.Linq;
using WeShop.Domain.Abstract;
using WeShop.Domain.Events;
using WeShop.Domain.Events.Catalog;
using WeShop.Domain.Exceptions;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Entities
{
    /// <summary>
    /// 产品品牌
    /// </summary>
    public class ProductBrand : Entity
    {
        public ProductBrand()
        {
            Id = IdGen.Create();
            IsDeleted = false;
        }

        public ProductBrand(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }

        /// <summary>
        /// 设置删除状态
        /// </summary>
        /// <param name="isDeleted"></param>
        public void SetDelete(bool isDeleted = false)
        {
            IsDeleted = isDeleted;
            if (IsDeleted)
                AddDomainEvent(new BrandDeleteDomainEvent(Id));
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new WeShopDomainException($"{nameof(name)} 不能为空。");
            Name = name;
        }
    }
}