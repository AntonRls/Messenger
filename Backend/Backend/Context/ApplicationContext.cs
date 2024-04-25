using Backend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Dialog> Dialogs => Set<Dialog>();
        public DbSet<Message> Messages => Set<Message>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
