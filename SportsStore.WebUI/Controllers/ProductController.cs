using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        public int PageSize = 4;
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(String category, int page = 1) {

            PagingInfo pi = new PagingInfo();
            pi.ItemsPerPage = PageSize;
            pi.CurrentPage = page;
            pi.TotalItems = repository.Products.Count();

            ProductsListViewModel vm = new ProductsListViewModel();
            vm.PagingInfo = pi;
            vm.Products = repository.Products
                .Where(p => p.Category == category || category == null)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            return View(vm);

        }
    }
}