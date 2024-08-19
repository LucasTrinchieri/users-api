using Microsoft.EntityFrameworkCore;

namespace Users_api.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
