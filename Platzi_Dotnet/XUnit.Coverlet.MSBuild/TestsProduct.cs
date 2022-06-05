using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Platzi_Dotnet.Controllers;
using Platzi_Dotnet.Data;
using Platzi_Dotnet.Models;
using Platzi_Dotnet.VIewModels;
using System.Net;
using System.Web.Mvc;
using Xunit;

namespace Platzi_Dotnet.XUnit.Coverlet.MSBuild
{
    public class TestsProduct
    {

        private readonly IMapper mapper;
        List<Product> products = new List<Product>()
        {
            new Product { Description = "Description", Id = 1, Name = "name", Price = 50.0 },
            new Product { Description = "Description", Id = 2, Name = "name", Price = 30.0 }
        };

        List<ProductViewModel> productViews = new List<ProductViewModel>
        {
            new ProductViewModel { Description = "Description", Id = 1, Name = "name", Price = 50.0 },
            new ProductViewModel { Description = "Description", Id = 2, Name = "name", Price = 30.0 },
        };
        Mock<IMapper> mappingService = new Mock<IMapper>();
        public ApplicationDbContext DbContextInfo(List<Product>? products)
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DotNet")
            .Options;

            ApplicationDbContext context = new ApplicationDbContext(options);
            // Insert seed data into the database using one instance of the context
            
            if (products!=null) {
                context.AddRange(products);
            } 
            context.SaveChanges();
            return context;
            
        }

        [Fact]
        public async Task GetProducts_Success()
        {
            //arrange
            mappingService.Setup(m => m.Map<IEnumerable<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(productViews);
            var productController = new ProductsController(DbContextInfo(products), mappingService.Object);

            //act
            var result = await productController.GetProducts();


            //Assert
            Assert.NotNull(result);
            var response = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetProducts_NotFound()
        {
            //arrange
            mappingService.Setup(m => m.Map<IEnumerable<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(productViews);
            var productController = new ProductsController(DbContextInfo(null), mappingService.Object);

            //act
            var result = await productController.GetProducts();


            //Assert
            Assert.NotNull(result);
            var response = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task GetProducts_BadRequest()
        {
            //arrange
            mappingService.Setup(m => m.Map<IEnumerable<ProductViewModel>>(It.IsAny<List<Product>>())).Returns(new List<ProductViewModel>());
            var productController = new ProductsController(DbContextInfo(products), mappingService.Object);

            //act
            var result = await productController.GetProducts();


            //Assert
            Assert.NotNull(result);
            var response = Assert.IsType<ConflictResult>(result.Result);
            Assert.Equal(409, response.StatusCode);

        }
    }
}
