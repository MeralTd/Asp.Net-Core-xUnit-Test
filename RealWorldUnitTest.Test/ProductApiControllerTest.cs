﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWorldUnitTest.Web.Controllers;
using RealWorldUnitTest.Web.Models;
using RealWorldUnitTest.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RealWorldUnitTest.Test
{
    public class ProductApiControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsApiController _controller;
        private List<Product> products;

        public ProductApiControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsApiController(_mockRepo.Object);

            products = new List<Product>()
            {
                new Product {Id =1, Name= "Kalem", Price=100 ,Stock =50, Color ="Kırmızı"},
                new Product {Id =2, Name= "Defter", Price=200 ,Stock =500, Color ="Mavi"},

            };
        }

        [Fact]
        public async void GetProducts_ActionExecutes_ReturnOkResultWithProduct()
        {
            _mockRepo.Setup(x => x.GetAll()).ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);

            Assert.Equal<int>(2, returnProducts.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetProduct_IdInValid_ReturnNotFound(int productId)
        {
            Product product = null;

            _mockRepo.Setup(x => x.GetById(productId)).ReturnsAsync(product);
            var result = await _controller.GetProduct(productId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetProduct_IdValid_ReturnOkResult(int productId)
        {
            var product = products.First(x=>x.Id==productId);

            _mockRepo.Setup(x => x.GetById(productId)).ReturnsAsync(product);

            var result = await _controller.GetProduct(productId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProduct = Assert.IsType<Product>(okResult.Value);

            Assert.Equal(productId, returnProduct.Id);
            Assert.Equal(product.Name, returnProduct.Name);
        }

        [Theory]
        [InlineData(1)]
        public void PutProduct_IdIsNotEqualProduct_ReturnBadRequestResult(int productId)
        {
            var product = products.First(x => x.Id == productId);

            var result = _controller.PutProduct(2, product);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void PutProduct_ActionExecutes_ReturnNoContent(int productId)
        {
            var product = products.First(x => x.Id == productId);

            _mockRepo.Setup(r => r.Update(product));

            var result = _controller.PutProduct(productId, product);

            _mockRepo.Verify(r => r.Update(product), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
