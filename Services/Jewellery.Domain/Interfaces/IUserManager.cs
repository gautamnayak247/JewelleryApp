using Jewellery.Domain.Entities;
using System.Threading.Tasks;

namespace Jewellery.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserByIdAsync(int id);
    }
}