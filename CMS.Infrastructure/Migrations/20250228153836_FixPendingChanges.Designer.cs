﻿// <auto-generated />
using System;
using CMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMS.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250228153836_FixPendingChanges")]
    partial class FixPendingChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CMS.Domain.Admin.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Shirts",
                            Slug = "shirts"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Fruits",
                            Slug = "fruits"
                        });
                });

            modelBuilder.Entity("CMS.Domain.Admin.Pages.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Pages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "This is the Home Page",
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Order = 100,
                            Slug = "home",
                            Title = "Home"
                        },
                        new
                        {
                            Id = 2,
                            Body = "This is the About Page",
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Order = 100,
                            Slug = "about",
                            Title = "About"
                        },
                        new
                        {
                            Id = 3,
                            Body = "This is the Services Page",
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Order = 100,
                            Slug = "services",
                            Title = "Services"
                        },
                        new
                        {
                            Id = 4,
                            Body = "This is the Contact Page",
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Order = 100,
                            Slug = "contact",
                            Title = "Contact"
                        });
                });

            modelBuilder.Entity("CMS.Domain.Admin.Products.Gallery.ProductGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsGallery");
                });

            modelBuilder.Entity("CMS.Domain.Admin.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 2,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Juicy Appels",
                            Image = "apple1.jpg",
                            Name = "Appel",
                            Price = 1.50m,
                            Slug = "appel"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Juicy Banana",
                            Image = "grapes3.jpg",
                            Name = "Banana",
                            Price = 2m,
                            Slug = "banana"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Juicy Grapers",
                            Image = "mand1.jpg",
                            Name = "Grapers",
                            Price = 2.5m,
                            Slug = "grapers"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Juicy Oranges",
                            Image = "mand2.jpg",
                            Name = "Oranges",
                            Price = 25.5m,
                            Slug = "oranges"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Nice Blue Shirt",
                            Image = "blue1.jpg",
                            Name = "Blue Shirt",
                            Price = 15.5m,
                            Slug = "blue-shirt"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Nice Yellow Shirt",
                            Image = "yellow1.png",
                            Name = "Yellow Shirt",
                            Price = 55.5m,
                            Slug = "yellow-shirt"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Decription = "Nice green Shirt",
                            Image = "green4.png",
                            Name = "Green Shirt",
                            Price = 65.5m,
                            Slug = "green-shirt"
                        });
                });

            modelBuilder.Entity("CMS.Domain.Admin.Products.Gallery.ProductGallery", b =>
                {
                    b.HasOne("CMS.Domain.Admin.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CMS.Domain.Admin.Products.Product", b =>
                {
                    b.HasOne("CMS.Domain.Admin.Categories.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CMS.Domain.Admin.Categories.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
