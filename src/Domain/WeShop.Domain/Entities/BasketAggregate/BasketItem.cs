using System;
using WeShop.Domain.Abstract;

namespace WeShop.Domain.Entities.BasketAggregate
{
    public class BasketItem : Entity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}