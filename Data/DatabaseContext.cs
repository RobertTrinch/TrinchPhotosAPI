using Microsoft.EntityFrameworkCore;
using TrinchPhotos_Web.Database.Models;

namespace TrinchPhotosAPI.Data
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Galleries> Galleries { get; set; }
        public DbSet<Creators> Creators { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public string DbPath { get; }

        public DatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite($"Data Source=sqlite-database-path");
            optionsBuilder.UseNpgsql("postgresql-db-connectionstring");
        }

    }
}
