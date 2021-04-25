using Jewellery.Application.Models.v1;
using Jewellery.Domain.Entities;
using System.Threading.Tasks;

namespace Jewellery.Application.Interfaces
{
    public interface ILogInService
    {
        Task<AuthToken> GetTokenAsync(LogInModel model);
    }
}