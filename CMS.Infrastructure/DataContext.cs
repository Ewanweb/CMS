using CMS.Domain.Admin.Categories;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Products;
using CMS.Domain.Admin.Products.Gallery;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGallery> ProductsGallery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Shirts", Slug = "shirts" , CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0) },
                    new Category { Id = 2, Name = "Fruits", Slug = "fruits",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Name = "Appel",
                        Slug = "appel",
                        Decription = "Juicy Appels",
                        Price = 1.50M,
                        CategoryId = 2,
                        Image = "apple1.jpg",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)

                    },

                    new Product
                    {
                        Id = 2,
                        Name = "Banana",
                        Slug = "banana",
                        Decription = "Juicy Banana",
                        Price = 2M,
                        CategoryId = 2,
                        Image = "grapes3.jpg",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)

                    },

                    new Product
                    {
                        Id = 3,
                        Name = "Grapers",
                        Slug = "grapers",
                        Decription = "Juicy Grapers",
                        Price = 2.5M,
                        CategoryId = 2,
                        Image = "mand1.jpg",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)

                    },

                    new Product
                    {
                        Id = 4,
                        Name = "Oranges",
                        Slug = "oranges",
                        Decription = "Juicy Oranges",
                        Price = 25.5M,
                        CategoryId = 2,
                        Image = "mand2.jpg",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)

                    },

                    new Product
                    {
                        Id = 5,
                        Name = "Blue Shirt",
                        Slug = "blue-shirt",
                        Decription = "Nice Blue Shirt",
                        Price = 15.5M,
                        CategoryId = 1,
                        Image = "blue1.jpg",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    },

                    new Product
                    {
                        Id = 6,
                        Name = "Yellow Shirt",
                        Slug = "yellow-shirt",
                        Decription = "Nice Yellow Shirt",
                        Price = 55.5M,
                        CategoryId = 1,
                        Image = "yellow1.png",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    },

                    new Product
                    {
                        Id = 7,
                        Name = "Green Shirt",
                        Slug = "green-shirt",
                        Decription = "Nice green Shirt",
                        Price = 65.5M,
                        CategoryId = 1,
                        Image = "green4.png",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    }
                );

            modelBuilder.Entity<Page>().HasData(
                    new Page { Id = 1, Title = "Home", Slug = "home", Body = "This is the Home Page",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    },
                    new Page { Id = 2, Title = "About", Slug = "about", Body = "This is the About Page",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    },
                    new Page { Id = 3, Title = "Services", Slug = "services", Body = "This is the Services Page",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)
                    },
                    new Page
                    {
                        Id = 4, Title = "Contact", Slug = "contact", Body = "This is the Contact Page",
                        CreatedTime = new DateTime(2024, 1, 1, 0, 0, 0)

                    }
                );
        }
    }
}
