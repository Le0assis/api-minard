using Microsoft.EntityFrameworkCore;
using minard_teste_2.models;

namespace minard_teste_2
{
    public class PetShopContext : DbContext

    {
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Dono> Donos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string databasePath = "petshop.db";
            options.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Dono)
                .WithMany(d => d.Animais)
                .HasForeignKey(a => a.DonoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
