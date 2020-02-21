using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace PictureDepot.Data
{
    public class PictureDepotContext : DbContext
    {
        public PictureDepotContext() : base("name=PictureDepotContext")
        {

        }
        public System.Data.Entity.DbSet<PictureDepot.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<PictureDepot.Models.Artist> Artists { get; set; }

    }
}