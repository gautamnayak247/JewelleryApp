namespace Jewellery.Test.Api.Controllers.v1
{
    using Jewellery.Api.Controllers.v1;
    using Jewellery.Application.Interfaces;
    using Jewellery.Application.Models.v1;
    using Jewellery.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Xunit;
    public class LogInControllerTest
    {
        private readonly LogInController sut;
        private readonly Mock<ILogInService> mockService;
        public LogInControllerTest()
        {
            mockService = new Mock<ILogInService>();
            sut = new LogInController(mockService.Object);
        }
        [Fact]
        public async Task Login_ShouldReturn_Ok()
        {
            var model = new LogInModel { UserId = "gautam", Password = "gautam123" };
            var authToken = new Domain.Entities.AuthToken { Token = Guid.NewGuid().ToString() };
            mockService.Setup(x => x.GetTokenAsync(model)).ReturnsAsync(authToken);
            var result = await sut.Post(model).ConfigureAwait(false);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturn_UnAuthorized()
        {
            var model = new LogInModel { UserId = "gautam", Password = "gautam123" };
            var authToken = default(AuthToken);
            mockService.Setup(x => x.GetTokenAsync(model)).ReturnsAsync(authToken);
            var result = await sut.Post(model).ConfigureAwait(false);
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
