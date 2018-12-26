using System;

namespace WeShop.Domain.Exceptions
{
    public class WeShopDomainException : Exception
    {
        public WeShopDomainException()
        {
        }

        public WeShopDomainException(string message)
            : base(message)
        {
        }

        public WeShopDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}