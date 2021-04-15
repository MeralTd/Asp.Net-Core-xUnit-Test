using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Controllers;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTestWithSqlServerLocalDb : ProductControllerTest
    {
        public ProductControllerTestWithSqlServerLocalDb()
        {
            var sqlConnection = @"Server=(localdb)\MSSQLLocalDB;Database=TestDB;Trusted_Connection=true;MultipleActiveResultSets=true";


            SetContextOptions(new DbContextOptionsBuilder<UnitTestDBContext>()
                .UseSqlServer(sqlConnection).Options);
        }

        [Fact]
        public async Task Create_ModelValidProduct_ReturnToActionWithSaveProduct()
        {
            var newProduct = new Product { Name = "Kalem 4", Price = 200, Stock = 100 ,Color="Mavi"};
            using(var context = new UnitTestDBContext(_contextOptions))
            {
                var category = context.Categories.First();

                newProduct.CategoryId = category.Id;

                var controller = new ProductsController(context);

                var result = await controller.Create(newProduct);

                var redirect = Assert.IsType<RedirectToActionResult>(result);

                Assert.Equal("Index", redirect.ActionName);
            }

            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var product = context.Products.First(x => x.Name == newProduct.Name);
                Assert.Equal(newProduct.Name, product.Name);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeletedAllProducts(int categoryId)
        {
            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var category = await context.Categories.FindAsync(categoryId);
                Assert.NotNull(category);

                context.Categories.Remove(category);
                context.SaveChanges();
            }

            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var products = await context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();

                Assert.Empty(products);
            }

        }
    }
}
