using Microsoft.EntityFrameworkCore;

namespace ConsoleAppModul25_EntityFramework
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        //// Объекты таблицы Companies
        //public DbSet<Company> Companies { get; set; }

        public AppContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TERMSRV01;Database=EF_ts;Trusted_Connection=True;");
        }
    }
}
