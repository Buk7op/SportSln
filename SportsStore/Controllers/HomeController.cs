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
        public ViewResult Index(string category, int productPage = 1)
              => View(new ProductListViewModel
              {
                  Products = repo.Products
                  .Where(p => category == null || p.Category == category)
                  .OrderBy(p => p.ProductID)
                  .Skip((productPage - 1) * productsOnPage)
                  .Take(productsOnPage),
                  PagingInfo = new PagingInfo
                  {
                      CurrentPage = productPage,
                      ItemPerPage = productsOnPage,
                      TotalItems = category == null ? repo.Products.Count() : repo.Products.Where(p => p.Category == category).Count()
                  },
                  CurrentCategory = category
              });
    }
}
