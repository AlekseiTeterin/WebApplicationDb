using Microsoft.EntityFrameworkCore;
using System;
using WebApplicationDB.Data;


namespace WebApplicationDB.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}
