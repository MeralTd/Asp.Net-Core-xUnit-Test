using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTestWithInMemory:ProductControllerTest
    {
        public ProductControllerTestWithInMemory()
        {
            SetContextOptions(new DbContextOptionsBuilder<UnitTestDBContext>()
                .UseInMemoryDatabase("UnitTestInMemoryDB").Options);
        }
    }
}
