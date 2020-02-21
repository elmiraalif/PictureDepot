using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PictureDepot.Data;
using PictureDepot.Models;
using System.Diagnostics;
using System.IO;




namespace PictureDepot.Controllers
{
    public class ProductsController : Controller
    {
        //Create the connection String
        private PictureDepotContext db = new PictureDepotContext();

        // GET request: Products/List
        //This ActionResult represents the URL above and lists all the Artists from the table
        public ActionResult List(string productSearchKeyword)
        {
            //we define the query first and then modify it for the condition
            string query = "SELECT * FROM Products";
            //there is a condition for search key, if the user is looking for a specific name,
            //the search result set appears if and only if the search keyword is not empty
            if (productSearchKeyword != "")
            {
                //The query below has been modified based on the keyword searched by user
                //it can target these properties Name, Code Year 
                query += " WHERE Name LIKE '%" + productSearchKeyword + "%' OR " +
                                 "Code LIKE '%" + productSearchKeyword + "%'  OR " +
                                 "Year LIKE '%" + productSearchKeyword + "%' ";
            }
            List<Product> products = db.Products.SqlQuery(query).ToList();
            return View(products);
        }//end of List ActionResult



        //Get request: Products/Add
        //This ActionResult represents the URL above and lists all the Artists from the table
        public ActionResult Add()
        {
            return View();
        }

        //This Add action is only responsible for Post requests
        [HttpPost]
        public ActionResult Add(string name, int code, int year, int artistId)
        {
            //set up the query: This line inserts into the database table with the following values
            string query = "INSERT INTO Products(Name, Code, Year, ArtistId) VALUES(@Name, @Code, @Year, @ArtistId)";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@Name", name);
            sqlparams[1] = new SqlParameter("@Code", code);
            sqlparams[2] = new SqlParameter("@Year", year);
            sqlparams[3] = new SqlParameter("@ArtistId", artistId);
            //The line
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //When inserting the data is finished it goes back to the list page
            return RedirectToAction("List");
        }


        //Get request: Product/Show/id
        public ActionResult Show()
        {

            return View();
        }

        //This is the post request of delete action:  
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //The query for delete a specific data
            string query = "DELETE FROM Products WHERE ProductId = @ProductId";
            //setting up the sql parameter: In this case we only have one column (id), so we don't need an array for sqlparameter
            SqlParameter sqlparam = new SqlParameter("@ProductId", id);
            //execute the query against the sqlparameter
            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }//end of controller
}