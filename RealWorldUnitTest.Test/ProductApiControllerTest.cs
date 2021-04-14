using Moq;
using RealWorldUnitTest.Web.Controllers;
using RealWorldUnitTest.Web.Models;
using RealWorldUnitTest.Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
    }
}
