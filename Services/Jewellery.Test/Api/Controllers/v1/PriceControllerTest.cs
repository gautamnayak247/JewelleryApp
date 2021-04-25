namespace Jewellery.Test.Api.Controllers.v1
{
    using Jewellery.Api.Controllers.v1;
    using Jewellery.Application.Interfaces;
    using Jewellery.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class PriceControllerTest
    {
        private readonly PriceController sut;
        private readonly Mock<ICalculationService> mockService;
        public PriceControllerTest()
        {
            mockService = new Mock<ICalculationService>();
            sut = new PriceController(mockService.Object);
        }
        [Fact]
        public async Task GetUserByIdTest_ShouldReturn_Ok()
        {
           //Arrange
            mockService
                .Setup(x => x.CalculatePrice(50, 2, 2))
                .Returns(new Price {TotalPrice = 98.00 });
            //Act
            var result = sut.Calculateprice(50, 2, 2);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
