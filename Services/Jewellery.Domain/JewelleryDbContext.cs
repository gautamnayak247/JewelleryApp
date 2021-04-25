namespace Jewellery.Domain
{
    using Jewellery.Domain.DBModels;
    using Microsoft.EntityFrameworkCore;

    public class JewelleryDbContext : DbContext
    {
        public JewelleryDbContext(DbContextOptions<JewelleryDbContext> options)
              : base(options)
        { }
        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<AppToken> AppToken { get; set; }
        public DbQuery<Entities.User> UserInformation { get; set; }
    }
}
