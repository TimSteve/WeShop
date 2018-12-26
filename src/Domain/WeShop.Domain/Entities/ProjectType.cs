using System.Collections.Generic;
using WeShop.Domain.Abstract;
using WeShop.Domain.Events;
using WeShop.Domain.Events.Catalog;
using WeShop.Domain.Exceptions;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Entities
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public class ProductType : Entity
    {
        public ProductType()
        {
            Id = IdGen.Create();
            IsDeleted = false;
        }

        public ProductType(string name) : this()
        {
            Name = name;
        }

        public string Name { get; private set; }
        public bool IsDeleted { get; private set; }

        public ICollection<Product> Products { get; set; }

        /// <summary>
        /// 设置删除状态
        /// </summary>
        /// <param name="isDeleted"></param>
        public void SetDelete(bool isDeleted = false)
        {
            IsDeleted = isDeleted;
            if (IsDeleted)
                AddDomainEvent(new TypeDeleteDomainEvent(Id));
        }

        /// <summary>
        /// 修改类型名称
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="WeShopDomainException"></exception>
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new WeShopDomainException($"{nameof(name)} 不能为空。");
            Name = name;
        }
    }
}