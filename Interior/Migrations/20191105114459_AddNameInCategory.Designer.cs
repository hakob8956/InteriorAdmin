﻿// <auto-generated />
using System;
using Interior.Models.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Interior.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20191105114459_AddNameInCategory")]
    partial class AddNameInCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("FileId");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Interior.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("FileId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Interior.Models.Entities.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<int>("CategoryId");

                    b.Property<int>("InteriorId");

                    b.Property<int>("LanguageId");

                    b.Property<int>("RecommendationId");

                    b.Property<int>("ShopId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InteriorId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("RecommendationId");

                    b.HasIndex("ShopId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("Interior.Models.Entities.FileStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Interior.Models.Entities.Interior", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AndroidBundleHref");

                    b.Property<bool>("Avaiable");

                    b.Property<int>("BrandId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DeepLinkingUrl");

                    b.Property<int?>("FileId");

                    b.Property<string>("GlbHref");

                    b.Property<string>("IosBundleHref");

                    b.Property<bool>("IsVisible");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FileId");

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

                    b.Property<int?>("FileId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Interior.Models.Entities.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("FileId");

                    b.Property<int?>("InteriorId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("ShopId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FileId");

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

                    b.Property<int?>("FileId");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

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

            modelBuilder.Entity("Interior.Models.Entities.Brand", b =>
                {
                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Brands")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Interior.Models.Entities.Category", b =>
                {
                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Categories")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Interior.Models.Entities.Content", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("Contents")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithMany("Contents")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Interior", "Interior")
                        .WithMany("Contents")
                        .HasForeignKey("InteriorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Language", "Language")
                        .WithMany("Contents")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Recommendation", "Recommendation")
                        .WithMany("Contents")
                        .HasForeignKey("RecommendationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("Contents")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Interior.Models.Entities.Interior", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("Interiors")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithMany("Interiors")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Interiors")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("Interiors")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Interior.Models.Entities.Language", b =>
                {
                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Languages")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Interior.Models.Entities.Recommendation", b =>
                {
                    b.HasOne("Interior.Models.Entities.Brand", "Brand")
                        .WithMany("Recommendations")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.Category", "Category")
                        .WithMany("Recommendations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Recommendations")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Interior.Models.Entities.Interior")
                        .WithMany("Recommendations")
                        .HasForeignKey("InteriorId");

                    b.HasOne("Interior.Models.Entities.Shop", "Shop")
                        .WithMany("Recommendations")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Interior.Models.Entities.Shop", b =>
                {
                    b.HasOne("Interior.Models.Entities.FileStorage", "File")
                        .WithMany("Shops")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull);
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
