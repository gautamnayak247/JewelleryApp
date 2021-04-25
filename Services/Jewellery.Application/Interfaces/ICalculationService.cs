using Jewellery.Domain.Entities;

namespace Jewellery.Application.Interfaces
{
    public interface ICalculationService
    {
        Price CalculatePrice(double goldPrice, double weight, double discount);
    }
}