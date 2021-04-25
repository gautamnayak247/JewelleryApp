namespace Jewellery.Test.Application.Services
{
    using Jewellery.Application.Interfaces;
    using Jewellery.Application.Models.v1;
    using Jewellery.Application.Services;
    using Jewellery.Domain.DBModels;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class LogInServiceTest
    {
        private readonly LogInService sut;
        private readonly Mock<ILogInManager> mockService;
        private readonly Mock<IGuidService> mockGuidService;
        public LogInServiceTest()
        {
            mockService = new Mock<ILogInManager>();
            mockGuidService = new Mock<IGuidService>();
            sut = new LogInService(mockService.Object, mockGuidService.Object);
        }
        [Fact]
        public async Task GetTokenAsync_ShouldReturn_Token()
        {
            //Arrange
            var token = Guid.NewGuid();
            var loginModel = new LogInModel
            {
                UserId = "gautam247",
                Password = "gautam123"
            };
            mockService
                .Setup(x => x.VerifyCredential(loginModel.UserId, loginModel.Password))
                .Returns((true, 1));
            mockService
               .Setup(x => x.GetTokenByUserId(1))
               .Returns(It.IsAny<AppToken>());
            mockGuidService.Setup(x => x.NewGuid()).Returns(token);
            mockService.Setup(x => x.SaveTokenAsync(1, token.ToString()));
            mockService.Verify();

            //Act
            var result = await sut.GetTokenAsync(loginModel).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AuthToken>(result);
            Assert.Equal(result.Token, token.ToString());
        }

        [Fact]
        public async Task GetTokenAsync_ShouldReturn_DefaultToken()
        {
            //Arrange
            var token = Guid.NewGuid();
            var loginModel = new LogInModel
            {
                UserId = "gautam247",
                Password = "gautam123"
            };

            mockService
                .Setup(x => x.VerifyCredential(loginModel.UserId, loginModel.Password))
                .Returns((false, 0));
            mockService.Verify();

            //Act
            var result = await sut.GetTokenAsync(loginModel).ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTokenAsync_ShouldReturn_ExistingToken()
        {
            //Arrange
            var token = Guid.NewGuid();
            var loginModel = new LogInModel
            {
                UserId = "gautam247",
                Password = "gautam123"
            };

            mockService
                .Setup(x => x.VerifyCredential(loginModel.UserId, loginModel.Password))
                .Returns((true, 1));
            mockService
                .Setup(x => x.GetTokenByUserId(1))
                .Returns(new AppToken
                {
                    Id = 1,
                    Token = token.ToString()
                });
            mockService.Verify();

            //Act
            var result = await sut.GetTokenAsync(loginModel).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<AuthToken>(result);
            Assert.Equal(result.Token, token.ToString());
        }
    }
}
