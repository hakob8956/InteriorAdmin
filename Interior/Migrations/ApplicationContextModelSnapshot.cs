﻿// <auto-generated />
using System;
using Interior.Models.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Interior.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Interior.Models.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Interior.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Interior.Models.Entities.CategoryAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<int?>("InteriorId");

                    b.Property<int?>("RecommendationId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InteriorId");

                    b.HasIndex("RecommendationId");

                    b.ToTable("CategoryAttachment");
                });

            modelBuilder.Entity("Interior.Models.Entities.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("ContentType");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Interior.Models.Entities.ContentAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId");

                    b.Property<int?>("CategoryId");

                    b.Property<int>("ContentId");

                    b.Property<int?>("InteriorId");

                    b.Property<int?>("RecommendationId");

                    b.Property<int?>("ShopId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContentId")
                        .IsUnique();

                    b.HasIndex("InteriorId");

                    b.HasIndex("RecommendationId");

                    b.HasIndex("ShopId");

                    b.ToTable("ContentAttachments");
                });

            modelBuilder.Entity("Interior.Models.Entities.FileStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<byte>("FileType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Interior.Models.Entities.FilesAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId");

                    b.Property<int?>("CategoryId");

                    b.Property<int>("FileId");

                    b.Property<int?>("InteriorId");

                    b.Property<int?>("LanguageId");

                    b.Property<int?>("RecommendationId");

                    b.Property<int?>("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId")
                        .IsUnique()
                        .HasFilter("[BrandId] IS NOT NULL");

                    b.HasIndex("CategoryId")
                        .IsUnique()
                        .HasFilter("[CategoryId] IS NOT NULL");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.HasIndex("InteriorId");

                    b.HasIndex("LanguageId")
                        .IsUnique()
                        .HasFilter("[LanguageId] IS NOT NULL");

                    b.HasIndex("RecommendationId")
                        .IsUnique()
                        .HasFilter("[RecommendationId] IS NOT NULL");

                    b.HasIndex("ShopId")
                        .IsUnique()
                        .HasFilter("[ShopId] IS NOT NULL");

                    b.ToTable("FilesAttachments");
                });

            modelBuilder.Entity("Interior.Models.Entities.Interior", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Avaiable");

                    b.Property<int?>("BrandId");

                    b.Property<string>("BuyUrl");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsVisible");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ShopId");

                    b.ToTable("Interiors");
                });

            modelBuilder.Entity("Interior.Models.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Interior.Models.Entities.OptionContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InteriorId");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Link");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("InteriorId");

                    b.HasIndex("LanguageId");

                    b.ToTable("OptionContents");
                });

            modelBuilder.Entity("Interior.Models.Entities.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("InteriorId");

                    b.Property<int>("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("InteriorId");

                    b.HasIndex("ShopId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("Interior.Models.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Interior.Models.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Interior.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<string>("Token");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username", "Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Interior.Models.Entities.Category", b =>
                {
                    b.HasOne("Interior.Models.Entities.Category", "Parent")
                        .WithMany("Parents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Interior.Models.Entities.CategoryAttachment", b =>
                {
                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithMany("CategoryAttachments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("CategoryAttachments")
                        .HasForeignKey("InteriorId");

                    b.HasOne("Interior.Models.Entities.Recommendation", "Recommendation")
                        .WithMany("CategoryAttachments")
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("Interior.Models.Entities.Content", b =>
                {
                    b.HasOne("Interior.Models.Entities.Language", "Language")
                        .WithMany("Content")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interior.Models.Entities.ContentAttachment", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("ContentsAttachment")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithMany("ContentsAttachment")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Content", "Content")
                        .WithOne("ContentAttachment")
                        .HasForeignKey("Interior.Models.Entities.ContentAttachment", "ContentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("ContentAttachments")
                        .HasForeignKey("InteriorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Recommendation", "Recommendation")
                        .WithMany("ContentsAttachment")
                        .HasForeignKey("RecommendationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("ContentsAttachment")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interior.Models.Entities.FilesAttachment", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("FilesAttachments")
                        .HasForeignKey("InteriorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Language", "Language")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Recommendation", "Recommendation")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "RecommendationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithOne("FilesAttachment")
                        .HasForeignKey("Interior.Models.Entities.FilesAttachment", "ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interior.Models.Entities.Interior", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("Interiors")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("Interiors")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Interior.Models.Entities.OptionContent", b =>
                {
                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("OptionContents")
                        .HasForeignKey("InteriorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Language", "Language")
                        .WithMany("OptionContents")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interior.Models.Entities.Recommendation", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("Recommendations")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("Recommendations")
                        .HasForeignKey("InteriorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("Recommendations")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Interior.Models.Entities.User", b =>
                {
                    b.HasOne("Interior.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
