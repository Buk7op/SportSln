using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;
namespace SportStore.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void CanUseRepository()
        {
            //Организация
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductID = 1, Name = "P1" },
            new Product {ProductID = 2, Name = "P2" }}).AsQueryable<Product>());
            HomeController homeController = new HomeController(mock.Object);
            //Действие
            IEnumerable<Product> result = (homeController.Index(null) as ViewResult).ViewData.Model as IEnumerable<Product>;
            //Утверждение
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);

        }
        [Fact]
        public void CanPaginate()
        {
            //Организация
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductID = 1, Name = "P1" },
            new Product {ProductID = 2, Name = "P2" },
            new Product {ProductID = 3, Name = "P3" },
            new Product {ProductID = 4, Name = "P4" },
            new Product {ProductID = 5, Name = "P5" }}).AsQueryable<Product>());
            HomeController homeController = new HomeController(mock.Object);
            homeController.productsOnPage = 3;
            //Действие
            IEnumerable<Product> result = (homeController.Index(null,1) as ViewResult).ViewData.Model as IEnumerable<Product>;
            //Утверждение
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 3);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P3", prodArray[2].Name);
        }
    }
}