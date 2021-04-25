using Jewellery.Application.Interfaces;
using Jewellery.Application.Models.v1;
using Jewellery.Domain.Entities;
using Jewellery.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Jewellery.Application.Services
{
    public class LogInService : ILogInService
    {
        private readonly ILogInManager logInManager;
        private readonly IGuidService guidService;

        public LogInService(ILogInManager _logInManager, IGuidService _guidService)
        {
            logInManager = _logInManager ?? throw new ArgumentNullException(nameof(_logInManager));
            guidService = _guidService ?? throw new ArgumentNullException(nameof(_guidService));
        }
        public async Task<AuthToken> GetTokenAsync(LogInModel model)
        {
            var authToken = default(AuthToken);
            var (isValid, userId) = logInManager.VerifyCredential(model.UserId, model.Password);
            if (isValid)
            {
                var token = default(string);
                var existingToken = logInManager.GetTokenByUserId(userId);
                if (existingToken != null)
                {
                    token = existingToken.Token;
                }
                else
                {
                    token = guidService.NewGuid().ToString();
                    await logInManager.SaveTokenAsync(userId, token)
                        .ConfigureAwait(false);

                }
                authToken = new AuthToken { Token = token };
            }
            return authToken;
        }
    }
}
