using Microsoft.AspNetCore.Mvc;
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
    public class ProductControllerTestWithInMemory:ProductControllerTest
    {
        public ProductControllerTestWithInMemory()
        {
            SetContextOptions(new DbContextOptionsBuilder<UnitTestDBContext>()
                .UseInMemoryDatabase("UnitTestInMemoryDB").Options);
        }

        [Fact]
        public async Task Create_ModelValidProduct_ReturnToActionWithSaveProduct()
        {
            var newProduct = new Product { Name = "Kalem 4", Price = 200, Stock = 100 };
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
    }
}
