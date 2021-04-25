using Jewellery.Domain.DBModels;
using System.Threading.Tasks;

namespace Jewellery.Domain.Interfaces
{
    public interface ILogInManager
    {
        (bool,int) VerifyCredential(string userName, string password);
        Task SaveTokenAsync(int userId, string apptoken);
        int? GetUserIdByToken(string token);
        AppToken GetTokenByUserId(int userId);
    }
}