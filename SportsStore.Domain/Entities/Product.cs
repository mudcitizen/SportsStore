using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public override String ToString()
        {
            return String.Format("Id - {0} ; Name {1} ; Description {2} ; Price {3} ; Category {4}", ProductID,Name,Description,Price,Category);
        }
    }

}
