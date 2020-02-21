using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PictureDepot.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //This line shows the many products to MANY categories
        public ICollection<Product> Products { get; set; }
    }
}