using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get {
                return context.Products;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            int count = Products.Count();
            sb.AppendLine(String.Format("Count - {0}", count));
            foreach (Product p in Products) {
                sb.AppendLine(p.ToString());
            }
            return sb.ToString();

        }
    }
}
