using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PictureDepot.Models
{
    public class Product
        /*A product has the following properties : The information below is associated with the column names of the Product Table
            id
            name
            code
            year
            This table also has connection with two other tables:
            artist and category tables
        */
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int Year { get; set; }

        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }

        //This line represents the one artist to MANY products relationship
        public ICollection<Artist>Artists { get; set; }
        //This one shows the MANY products to many categories
        public ICollection<Category> Categories { get; set; }


    }
}