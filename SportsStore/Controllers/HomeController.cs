using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
using SportsStore.Data;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repo;
        public int productsOnPage = 1;
        public HomeController(IStoreRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index(int productPage = 1)
              => View(new ProductListViewModel
              {
                  Products = repo.Products
                  .OrderBy(p => p.ProductID)
                  .Skip((productPage - 1) * productsOnPage)
                  .Take(productsOnPage),
                  PagingInfo = new PagingInfo
                  {
                      CurrentPage = productPage,
                      ItemPerPage = productsOnPage,
                      TotalItems = repo.Products.Count()
                  }
              });
    }
}
