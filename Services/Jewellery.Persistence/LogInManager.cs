namespace Jewellery.Persistence
{
    using Jewellery.Domain;
    using Jewellery.Domain.DBModels;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LogInManager : ILogInManager
    {
        private readonly JewelleryDbContext context;

        public LogInManager(JewelleryDbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        public (bool, int) VerifyCredential(string userName, string password)
        {
            try
            {
                var isValid = context.User.Where(user => user.UserId == userName && user.Password == password).Any();
                return (isValid, isValid ? context.User.Where(user => user.UserId == userName).First().Id : default);
            }
            catch (Exception ex)
            {
                throw new DBException(ex.Message, ex);
            }
        }

        public async Task SaveTokenAsync(int userId, string apptoken)
        {
            try
            {
                var token = new AppToken { Token = apptoken, UserId = userId };
                await context.AppToken.AddAsync(token).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new DBException(ex.Message, ex);
            }
        }
        public AppToken GetTokenByUserId(int userId)
        {
            try
            {
                return context.AppToken.Where(x => x.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBException(ex.Message, ex);
            }
        }
        public int? GetUserIdByToken(string token)
        {
            try
            {
                return context.AppToken.Where(x => x.Token == token).FirstOrDefault()?.UserId;
            }
            catch (Exception ex)
            {
                throw new DBException(ex.Message, ex);
            }
        }
    }
}
