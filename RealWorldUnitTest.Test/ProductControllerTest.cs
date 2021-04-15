using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTest
    {
        protected DbContextOptions<UnitTestDBContext> _contextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<UnitTestDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
            Seed();
        }

        public void Seed()
        {
            using(UnitTestDBContext context = new UnitTestDBContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Categories.Add(new Category { Name = "Kalemler" });
                context.Categories.Add(new Category { Name = "Defterler" });
                context.SaveChanges();

                context.Products.Add(new Product() { CategoryId = 1, Name = "Kalem 2", Price = 100, Stock = 100, Color = "Kırmızı" });
                context.Products.Add(new Product() { CategoryId = 1, Name = "Kalem 3", Price = 100, Stock = 100, Color = "Mavi" });
                context.SaveChanges();

            }
        }
    }
}
