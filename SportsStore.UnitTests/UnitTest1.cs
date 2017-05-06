using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Diagnostics;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act

            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            String htmlString = result.ToString();

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
htmlString);
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {

            const String cat1 = "Cat1";
            const String cat2 = "Cat2";

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = cat1},
                new Product {ProductID = 2, Name = "P2", Category = cat2},
                new Product {ProductID = 3, Name = "P3", Category = cat1},
                new Product {ProductID = 4, Name = "P4", Category = cat2},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            // Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(cat2, 1).Model;
            IEnumerable<Product> products = result.Products;

            // Assert
            Assert.IsTrue(products.Count() == 2);
            Assert.IsTrue(products.Where(p => p.Name == "P2").Count() == 1);
            Assert.IsTrue(products.Where(p => p.Name == "P4").Count() == 1);

        }

        [TestMethod]
        public void Can_Create_Categories()
        {

            const String cat1 = "Cat1";
            const String cat2 = "Cat2";
            const String cat3 = "Cat3";

            List<String> cats = new List<string>() { cat1, cat2, cat3 };

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = cat1},
                new Product {ProductID = 2, Name = "P2", Category = cat2},
                new Product {ProductID = 3, Name = "P3", Category = cat1},
                new Product {ProductID = 4, Name = "P4", Category = cat2},
                new Product {ProductID = 5, Name = "P5", Category = cat3}
            });

            // Arrange
            NavController controller = new NavController(mock.Object);

            // Act
            IEnumerable<String> result = (IEnumerable<String>)controller.Menu().Model;


            // Assert
            Assert.IsTrue(result.Count() == cats.Count());
            foreach (String cat in cats)
            {
                Assert.IsTrue(result.Where(r => r == cat).Count() == 1);

            }
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            const String cat1 = "Cat1";
            const String cat2 = "Cat2";
            const String cat3 = "Cat3";
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = cat1},
                new Product {ProductID = 2, Name = "P2", Category = cat2},
                new Product {ProductID = 3, Name = "P3", Category = cat1},
                new Product {ProductID = 4, Name = "P4", Category = cat2},
                new Product {ProductID = 5, Name = "P5", Category = cat3}
            });

            // Arrange
            NavController controller = new NavController(mock.Object);

            PartialViewResult pvr = controller.Menu(cat1);
            String actual = pvr.ViewBag.SelectedCategory;

            Assert.AreEqual(actual, cat1);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            // Arrange - create a controller and make the page size 3 items
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Action - test the product counts for different categories
            int res1 = ((ProductsListViewModel)target
                .List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target
                .List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target
                .List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target
                .List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

    }


}
