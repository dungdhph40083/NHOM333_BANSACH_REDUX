using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace AppData
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> Options) : base(Options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartsDetails { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            base.OnConfiguring(OptionsBuilder);
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("BookstoreDatabase"));
        }

        // wow all i had to do is to install ** Microsoft.Extensions.Configuration.Json **
    }
}
