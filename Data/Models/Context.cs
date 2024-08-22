using Microsoft.EntityFrameworkCore;

namespace CoreAndFood.Data.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //use sql server(veritabanı ismi DbCoreFood) integrated security;TrustServerCertificate
            optionsBuilder.UseSqlServer("DATABASE CONNECTİON STRİNG HERE");
        }

        public DbSet<Food> Foods { get; set; }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Admin> Admins { get; set; }



    }
}
