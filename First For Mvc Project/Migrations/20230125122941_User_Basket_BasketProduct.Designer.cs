﻿// <auto-generated />
using System;
using Pronia.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Pronia.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230125122941_User_Basket_BasketProduct")]
    partial class User_Basket_BasketProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Baskets", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.BasketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("BookId");

                    b.ToTable("BasketProducts", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Colors", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Navbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsViewOnFooter")
                        .HasColumnType("bit");

                    b.Property<bool>("IsViewOnHeader")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Navbars", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PaymentBenefits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNameInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PaymentBenefits", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubcategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Plants", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantColor", b =>
                {
                    b.Property<int>("PlantId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("PlantId", "ColorId");

                    b.HasIndex("ColorId");

                    b.ToTable("PlantColors", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNameInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PlantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlantId");

                    b.ToTable("PlantImages", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantSize", b =>
                {
                    b.Property<int>("PlantId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("PlantId", "SizeId");

                    b.HasIndex("SizeId");

                    b.ToTable("PlantSizes", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantTag", b =>
                {
                    b.Property<int>("PlantId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("PlantId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PlantTags", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sizes", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ButtonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ButtonURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNameInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Offer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sliders", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Categoryİd")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Categoryİd");

                    b.ToTable("Subcategories", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Subnavbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NavbarId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NavbarId");

                    b.ToTable("Subnavbars", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.UserActivation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ActivationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActivationUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserActivations", (string)null);
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Basket", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.User", "User")
                        .WithOne("Basket")
                        .HasForeignKey("First_For_Mvc_Project.Database.Models.Basket", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.BasketProduct", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Basket", "Basket")
                        .WithMany("BasketProducts")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("First_For_Mvc_Project.Database.Models.Plant", "Plant")
                        .WithMany("BasketProducts")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Plant", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Subcategory", "Subcategory")
                        .WithMany("Plants")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantColor", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Color", "Color")
                        .WithMany("PlantColors")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("First_For_Mvc_Project.Database.Models.Plant", "Plant")
                        .WithMany("PlantColors")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantImage", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Plant", "Plant")
                        .WithMany("PlantImages")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantSize", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Plant", "Plant")
                        .WithMany("PlantSizes")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("First_For_Mvc_Project.Database.Models.Size", "Size")
                        .WithMany("PlantSizes")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.PlantTag", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Plant", "Plant")
                        .WithMany("PlantTags")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("First_For_Mvc_Project.Database.Models.Tag", "Tag")
                        .WithMany("PlantTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Subcategory", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("Categoryİd")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Subnavbar", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.Navbar", "Navbar")
                        .WithMany("Subnavbars")
                        .HasForeignKey("NavbarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Navbar");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.UserActivation", b =>
                {
                    b.HasOne("First_For_Mvc_Project.Database.Models.User", "User")
                        .WithOne("UserActivation")
                        .HasForeignKey("First_For_Mvc_Project.Database.Models.UserActivation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Basket", b =>
                {
                    b.Navigation("BasketProducts");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Category", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Color", b =>
                {
                    b.Navigation("PlantColors");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Navbar", b =>
                {
                    b.Navigation("Subnavbars");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Plant", b =>
                {
                    b.Navigation("BasketProducts");

                    b.Navigation("PlantColors");

                    b.Navigation("PlantImages");

                    b.Navigation("PlantSizes");

                    b.Navigation("PlantTags");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Size", b =>
                {
                    b.Navigation("PlantSizes");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Subcategory", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.Tag", b =>
                {
                    b.Navigation("PlantTags");
                });

            modelBuilder.Entity("First_For_Mvc_Project.Database.Models.User", b =>
                {
                    b.Navigation("Basket");

                    b.Navigation("UserActivation");
                });
#pragma warning restore 612, 618
        }
    }
}
