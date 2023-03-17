using Microsoft.EntityFrameworkCore;

namespace Api_User.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) 
            : base(options) 
        { }

        public DbSet<User> Users { get; set; } = default!;
    }
}
