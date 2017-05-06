using SportsStore.Domain.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Nav
        public PartialViewResult Menu(String category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<String> cats = repository
                .Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(cat => cat);

            return PartialView(cats);
        }
    }
}