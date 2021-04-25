namespace Jewellery.Test.Application.Services
{
    using Jewellery.Application.Services;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTest
    {
        private readonly UserService sut;
        private readonly Mock<IUserManager> mockService;
        private readonly Mock<IUser> mockUserContext;
        public UserServiceTest()
        {
            mockService = new Mock<IUserManager>();
            mockUserContext = new Mock<IUser>();
            sut = new UserService(mockService.Object, mockUserContext.Object);
        }
        [Fact]
        public async Task GetUserAsync_ShouldReturnFromContext_User()
        {
            //Arrange
            var user = new User
            {
                UserId = "gautam247",
                Id = 1,
                FirstName = "Gautam"
            };
            mockUserContext
                .SetupGet(x => x.FirstName)
                .Returns("Gautam");

            //Act
            var result = await sut.GetUserAsync().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Gautam", user.FirstName);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturn_User()
        {
            //Arrange
            var user = new User
            {
                UserId = "gautam247",
                Id = 1,
                FirstName = "Gautam"
            };
            mockUserContext
                .SetupGet(x => x.FirstName)
                .Returns(It.IsAny<string>());
            mockUserContext
               .SetupGet(x => x.Id)
               .Returns(1);
            mockService
                .Setup(x => x.GetUserByIdAsync(1))
                .ReturnsAsync(user);

            //Act
            var result = await sut.GetUserAsync().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal("Gautam", user.FirstName);
        }
    }
}
