using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete.Tests
{
    [TestClass()]
    public class EFProductRepositoryTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            IProductRepository repo = new EFProductRepository();
            String s = repo.ToString();
            Debug.WriteLine(s);
        }
    }
}