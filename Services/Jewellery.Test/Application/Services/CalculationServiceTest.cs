namespace Jewellery.Test.Application.Services
{
    using Jewellery.Application.Services;
    using Jewellery.Domain.Entities;
    using Moq;
    using Xunit;

    public class CalculationServiceTest
    {
        private readonly CalculationService sut;
        private readonly Mock<IUser> mockUserContext;
        public CalculationServiceTest()
        {
            mockUserContext = new Mock<IUser>();
            sut = new CalculationService(mockUserContext.Object);
        }
        [Fact]
        public void CalculatePrice_ShouldReturn_Price_Privileged()
        {
            //Arrange
            mockUserContext
                .SetupGet(x => x.Type)
                .Returns("Privileged");

            //Act
            var result = sut.CalculatePrice(50.00, 2.00, 2);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Price>(result);
            Assert.Equal(98.00, result.TotalPrice);
        }

        [Fact]
        public void CalculatePrice_ShouldReturn_Price_Standard()
        {
            //Arrange
            mockUserContext
                .SetupGet(x => x.Type)
                .Returns("Standard");

            //Act
            var result = sut.CalculatePrice(50.00, 2.00, 2);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Price>(result);
            Assert.Equal(100.00, result.TotalPrice);
        }

    }
}
