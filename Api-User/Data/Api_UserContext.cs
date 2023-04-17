using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_User.Models;

namespace Api_User.Data
{
    public class Api_UserContext : DbContext
    {
        public Api_UserContext(DbContextOptions<Api_UserContext> options)
            : base(options)
        {
        }
        public DbSet<Api_User.Models.User> User { get; set; } = default!;

        public DbSet<Api_User.Models.Products> Products { get; set; } = default!;
        public DbSet<Api_User.Models.Category> Categories { get; set; } = default!;
        public DbSet<Api_User.Models.Compra2> Compra { get; set; } = default!;
    }
}
