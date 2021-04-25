namespace Jewellery.Application.Services
{
    using Jewellery.Application.Interfaces;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserManager userManager;
        private readonly IUser loggedInUserContext;

        public UserService(IUserManager _userManager, IUser _loggedInUserContext)
        {
            userManager = _userManager ?? throw new ArgumentNullException(nameof(_userManager));
            loggedInUserContext = _loggedInUserContext ?? throw new ArgumentNullException(nameof(_loggedInUserContext));
        }

        public async Task<IUser> GetUserAsync()
        {
            if (string.IsNullOrWhiteSpace(loggedInUserContext.FirstName))
            {
                return await userManager.GetUserByIdAsync(loggedInUserContext.Id).ConfigureAwait(false);
            }
            return loggedInUserContext;
        }
    }
}
