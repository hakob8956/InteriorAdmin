using Interior.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Interior.Models.EFContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { Database.Migrate(); }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Interior.Models.Entities.Interior> Interiors { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<FileStorage> Files { get; set; }
        public DbSet<OptionContent> OptionContents { get; set; }
        public DbSet<FilesAttachment> FilesAttachments { get; set; }
        public DbSet<ContentAttachment> ContentAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed
            //#region RoleSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = 1, Name = "admin"},
            //    new Role { Id = 2, Name = "user"}
            // );
            //#endregion
            //#region UserSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new User { Id = 1,Username="hakob8956",Email="hak.n@gmail.com",RoleId=1,FirstName="Hakob",LastName="Nersesyan",Password="hak123321"},
            //    new User { Id = 2,Username="hakon8558",Email="hak.n1@gmail.com",RoleId=2,FirstName="Hakob1",LastName="Nersesyan1",Password="hasdasdasdas12312"}
            //    );
            //#endregion
            //#region FilesSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new FileStorage { Id = 1, Name = "File1", Path = "unknown" },
            //    new FileStorage { Id = 2, Name = "File2", Path = "unknown" }
            //    );
            //#endregion
            //#region LanguagesSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Language { Id=1,Name="English",Code="eng"},
            //    new Language { Id = 2, Name = "Russian", Code = "ru" }
            //    );
            //#endregion
            //#region ShopSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Shop { Id=1,FileId=1},
            //    new Shop { Id=2}
            //    );
            //#endregion
            //#region BrandSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Brand { Id = 1, FileId = 1 },
            //    new Brand { Id = 2 }
            //    );
            //#endregion
            //#region CategorySeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Category { Id = 1, FileId = 1 },
            //    new Category { Id = 2 }
            //    );
            //#endregion
            //#region ContentSeed
            //modelBuilder.Entity<Role>().HasData(
            //    new Content { Id = 1,Text="Test",LanguageId=1,BrandId=1,CategoryId=1,ShopId=1,InteriorId=1 },
            //    new FileStorage { Id = 2, Name = "File2", Path = "unknown" }
            //    );
            //#endregion
            #endregion
            //Configurations
            modelBuilder.ApplyConfiguration(new Entities.Interior.InteriorMapping());
            modelBuilder.ApplyConfiguration(new Recommendation.RecommendationMapping());
            modelBuilder.ApplyConfiguration(new User.UserMapping());
            modelBuilder.ApplyConfiguration(new Content.ContentMapping());
            modelBuilder.ApplyConfiguration(new OptionContent.OptionContentMapping());
            modelBuilder.ApplyConfiguration(new FilesAttachment.FilesAttachmentMapping());
            modelBuilder.ApplyConfiguration(new ContentAttachment.ContentAttachmentMapping());
            modelBuilder.ApplyConfiguration(new Category.CategoryMapping());
        }
    }
}
