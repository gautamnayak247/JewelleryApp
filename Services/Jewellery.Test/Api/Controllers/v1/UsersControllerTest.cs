namespace Jewellery.Test.Api.Controllers.v1
{
    using Jewellery.Api.Controllers.v1;
    using Jewellery.Application.Interfaces;
    using Jewellery.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class UsersControllerTest
    {
        private readonly UsersController sut;
        private readonly Mock<IUserService> mockService;
        public UsersControllerTest()
        {
            mockService = new Mock<IUserService>();
            sut = new UsersController(mockService.Object);
        }
        [Fact]
        public async Task GetUserByIdTest_ShouldReturn_Ok()
        {
            var user = new User
            {
                FirstName = "Gautam",
                Id = 1,
                LastName = "Nayak",
                Type = "Privileged",
                UserId = "gautam247"
            };
            mockService.Setup(x => x.GetUserAsync()).ReturnsAsync(user);
            var result = await sut.GetUser().ConfigureAwait(false);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
