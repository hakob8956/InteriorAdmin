using Interior.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Interior.Models.EFContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {/* Database.EnsureCreated();*/ }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Interior.Models.Entities.Interior> Interiors { get; set; }
        public DbSet<Shop> Shops { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Role>(s => s.Role)
                .WithMany(s => s.Users)
                .HasForeignKey(s => s.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            //For languages

          
            modelBuilder.Entity<Content>()
                .HasOne<Language>(s => s.Language)
                .WithMany(s => s.Contents)
                .HasForeignKey(s => s.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
                .HasOne<Category>(s => s.Category)
                .WithMany(s => s.Contents)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
              .HasOne<Shop>(s => s.Shop)
              .WithMany(s => s.Contents)
              .HasForeignKey(s => s.ShopId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
              .HasOne<Brand>(s => s.Brand)
              .WithMany(s => s.Contents)
              .HasForeignKey(s => s.BrandId)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Content>()
              .HasOne<Recommendation>(s => s.Recommendation)
              .WithMany(s => s.Contents)
              .HasForeignKey(s => s.RecommendationId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
             .HasOne<Interior.Models.Entities.Interior>(s => s.Interior)
             .WithMany(s => s.Contents)
             .HasForeignKey(s => s.InteriorId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne<Content>(s => s.Name)
                .WithMany(s => s.Categories)
                .HasForeignKey(s => s.NameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shop>()
               .HasOne<Content>(s => s.Name)
               .WithMany(s => s.Shops)
               .HasForeignKey(s => s.NameId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Brand>()
               .HasOne<Content>(s => s.Name)
               .WithMany(s => s.Brands)
               .HasForeignKey(s => s.NameId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recommendation>()
               .HasOne<Content>(s => s.Name)
               .WithMany(s => s.Recommendations)
               .HasForeignKey(s => s.NameId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interior.Models.Entities.Interior>()
            .HasOne<Content>(s => s.Name)
            .WithMany(s => s.InteriorsNames)
            .HasForeignKey(s => s.NameId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interior.Models.Entities.Interior>()
           .HasOne<Content>(s => s.Description)
           .WithMany(s => s.InteriorsDescriptions)
           .HasForeignKey(s => s.DescriptionId)
           .OnDelete(DeleteBehavior.Restrict);

            //Interior
            modelBuilder.Entity<Interior.Models.Entities.Interior>()
              .HasOne<Brand>(s => s.Brand)
              .WithMany(s => s.Interiors)
              .HasForeignKey(s => s.BrandId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interior.Models.Entities.Interior>()
             .HasOne<Shop>(s => s.Shop)
             .WithMany(s => s.Interiors)
             .HasForeignKey(s => s.ShopId)
             .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Interior.Models.Entities.Interior>()
            // .HasOne<Category>(s => s.Category)
            // .WithMany(s => s.Interiors)
            // .HasForeignKey(s => s.CategoryId)
            // .OnDelete(DeleteBehavior.Restrict);
        
            //Reccommendation
            modelBuilder.Entity<Recommendation>()
                .HasOne<Category>(s => s.Category)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recommendation>()
                .HasOne<Brand>(s => s.Brand)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recommendation>()
                .HasOne<Shop>(s => s.Shop)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(s => s.ShopId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
