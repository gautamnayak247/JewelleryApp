namespace Jewellery.Application.Services
{
    using Jewellery.Application.Interfaces;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Enums;
    using System;

    public class CalculationService : ICalculationService
    {
        private readonly IUser loggedInUserContext;

        public CalculationService(IUser _loggedInUserContext)
        {
            loggedInUserContext = _loggedInUserContext ?? throw new ArgumentNullException(nameof(loggedInUserContext));
        }
        public Price CalculatePrice(double goldPrice, double weight, double discount)
        {
            var price = new Price();
            var totalPrice = goldPrice * weight;
            if (loggedInUserContext.Type.Equals(PrivilegeType.Privileged.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                totalPrice -= totalPrice * (discount / 100);
            }
            price.TotalPrice = totalPrice;
            return price;
        }
    }
}
