namespace Jewellery.Persistence
{
    using Jewellery.Domain;
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using static Jewellery.Domain.Constant;

    public class UserManager : IUserManager
    {
        private readonly JewelleryDbContext context;

        public UserManager(JewelleryDbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var query = string.Format(CultureInfo.InvariantCulture, Template, id);
                var user = await context.Query<User>().FromSql(query).ToListAsync();
                return user.FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new DBException(ex.Message, ex);
            }
        }
    }
}
