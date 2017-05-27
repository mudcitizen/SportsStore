using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }


        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public override String ToString()
        {
            return String.Format("Id - {0} ; Name - {1} ; Description - {2} ; Price - {3} ; Category - {4} ; ImageMimeType - {5}", ProductID,Name,Description,Price,Category,ImageMimeType);
        }
    }

}
