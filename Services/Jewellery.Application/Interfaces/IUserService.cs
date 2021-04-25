using Jewellery.Domain.Entities;
using System.Threading.Tasks;

namespace Jewellery.Application.Interfaces
{
    public interface IUserService
    {
        Task<IUser> GetUserAsync();
    }
}