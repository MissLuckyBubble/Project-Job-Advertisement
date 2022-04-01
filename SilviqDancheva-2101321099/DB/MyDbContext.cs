using Microsoft.EntityFrameworkCore;
using SilviqDancheva_2101321099.Entities;
namespace SilviqDancheva_2101321099.DB
{
    public class MyDbContext : DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<JobAd> JobAds { get; set; }
        public DbSet<UserToJobAd> UserToJobAds { get; set; }
        public DbSet<Category> Categories { get; set; }

        public MyDbContext()
        {
            this.Users = this.Set<User>();
            this.Roles = this.Set<Role>();
            this.JobAds = this.Set<JobAd>();
            this.UserToJobAds = this.Set<UserToJobAd>();
            this.Categories = this.Set<Category>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=SilviqDanchevaDB;Trusted_Connection=True;").UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id= 1,
                RoleName = "employer"
            });
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 2,
                RoleName = "employee"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id= 1,
                CategoryName = "Пълно работно време"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 2,
                CategoryName = "Почасова"
            }); 
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 3,
                CategoryName = "Подходяща за студенти"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 4,
                CategoryName = "Търговия и Продажби"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 5,
                CategoryName = "Инженери и Техници"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 6,
                CategoryName = "Архитектура, Строителство"
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 7,
                CategoryName = "Маркетинг, Реклама, ПР"
            });
        }

    }
}