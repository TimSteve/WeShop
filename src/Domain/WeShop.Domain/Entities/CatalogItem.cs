using System;
using WeShop.Domain.Abstract;
using WeShop.Domain.Exceptions;
using WeShop.Infrasture.Common;

namespace WeShop.Domain.Entities
{
    /// <summary>
    /// 产品
    /// </summary>
    public class Product : Entity, IAggregateRoot
    {
        public Product()
        {
            Id = IdGen.Create();
            IsDeleted = false;
        }

        public Product(
            string name,
            string description,
            decimal price,
            string pictureFileName,
            string pictureUri,
            long productBrandId,
            long productTypeId,
            bool isPublished) : this()
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            ProductBrandId = productBrandId;
            ProductTypeId = productTypeId;
            IsPublished = isPublished;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public string PictureFileName { get; private set; }

        public string PictureUri { get; private set; }

        public long ProductTypeId { get; private set; }
        public ProductType ProductType { get; set; }

        public long ProductBrandId { get; private set; }
        public ProductBrand ProductBrand { get; set; }

        public bool IsDeleted { get; private set; }
        public bool IsPublished { get; private set; }

        /// <summary>
        /// 设置删除状态
        /// </summary>
        /// <param name="isDeleted"></param>
        public void SetDelete(bool isDeleted = false)
        {
            IsDeleted = isDeleted;
        }

        /// <summary>
        /// 设置发布状态
        /// </summary>
        /// <param name="isPublished"></param>
        public void SetPublish(bool isPublished = true)
        {
            IsPublished = isPublished;
        }

        /// <summary>
        /// 设置品牌
        /// </summary>
        /// <param name="brandId"></param>
        public void SetBrand(long brandId)
        {
            ProductBrandId = brandId;
        }

        /// <summary>
        /// 设置类型
        /// </summary>
        /// <param name="type"></param>
        public void SetType(long typeId)
        {
            ProductTypeId = typeId;
        }

        public void BeUpdate(string name, string description, decimal price, string picUri, string pictureFileName,
            long productBrandId, long productTypeId)
        {
            if (string.IsNullOrEmpty(name))
                throw new WeShopDomainException($"{nameof(name)} 不能为空。");
            Name = name;
            Description = description;
            Price = price;
            PictureUri = picUri;
            PictureFileName = pictureFileName;
            ProductBrandId = productBrandId;
            ProductTypeId = productTypeId;
        }
    }
}