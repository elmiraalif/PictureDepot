using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PictureDepot.Data;
using PictureDepot.Models;
using System.Diagnostics;

namespace PictureDepot.Controllers
{
    public class ArtistsController : Controller
    {
        //Create the connection String
        private PictureDepotContext db = new PictureDepotContext();
        // GET: Artists/List
        //This ActionResult represents the URL above and lists all the Artists from the table
        public ActionResult List(string artistSearchKeyword)
        {
            //This query displays all of the records in the table
            string query = "SELECT * FROM Artists";
            //if the user is looking for a search keyword, 
            // The search result is base on all of the properties of the table Artist by customizing the query 
            if (artistSearchKeyword != "")
            {
                query += " WHERE FirstName LIKE '%" + artistSearchKeyword + "%' OR " +
                         "LastName LIKE '%" + artistSearchKeyword + "%'  OR " +
                         "BirthDate LIKE '%" + artistSearchKeyword + "%' OR " +
                         "HireDate LIKE '%" + artistSearchKeyword + "%' OR " +
                         "Email LIKE '%" + artistSearchKeyword + "%' OR " +
                         "Phone LIKE '%" + artistSearchKeyword + "%'";
            }
            List<Artist> artists = db.Artists.SqlQuery(query).ToList();
            return View(artists);
        }//end of List ActionResult


        //Get: Artists/Add
        public ActionResult Add()
        {
            return View();
        }

        //This Add method handles the form request 
        [HttpPost]
        public ActionResult Add(string firstName, string lastName, string birthDate, string hireDate, string email, string phone)
        {
            //setup the query string to add the input value fields of user into the database table
            string query = "INSERT INTO Artists(FirstName, LastName, BirthDate, HireDate, Email, Phone) VALUES (@FirstName, @LastName, @BirthDate, @HireDate, @Email, @Phone)";
            //Set up the sql parameter which maps to dataset columns 
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@FirstName", firstName);
            sqlparams[1] = new SqlParameter("@LastName", lastName);
            sqlparams[2] = new SqlParameter("@BirthDate", birthDate);
            sqlparams[3] = new SqlParameter("@HireDate", hireDate);
            sqlparams[4] = new SqlParameter("@Email", email);
            sqlparams[5] = new SqlParameter("@Phone", phone);
            //run the query against sqlparams
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //back to list of artists as it's finished
            return RedirectToAction("List");
        }



        //the post request => the action of deleting
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //set up the query
            string query = "DELETE FROM Artists WHERE ArtistId = @ArtistId";
            //instantiate the built-in class of sqlparameter 
            SqlParameter sqlparam = new SqlParameter("@ArtistId", id);
            //Run the query against the sql parameter
            db.Database.ExecuteSqlCommand(query, sqlparam);
            //go back to the list of Artists
            return RedirectToAction("List");
        }


    //the Get request for showing the specific Artist
        public ActionResult Show(int id)
        {
            //Set up the query to grab the data for the targeted artist
            string query = "SELECT * FROM Artists WHERE ArtistId = @ArtistId";
            //sqlparameter to map the data 
            var parameter = new SqlParameter("@ArtistId", id);
            //run the query
            //Use the FirstOrDefault to get only one row from the resultset
            Artist selectedArtist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();
            //display the artist
            return View(selectedArtist);
        }

        //Get request of Update => takes the user to the page of Update, with populated data inside the input fields
        public ActionResult Update(int id)
        {
            //Set up the query to select all columns of a specific row (The selected artist)
            string query = "SELECT * FROM Artists where ArtistId = @ArtistId";
            var parameter = new SqlParameter("@ArtistId", id);
            Artist selectedArtist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedArtist);
        }

        //post request of Update to make changes : The action of Updating the data
        [HttpPost]
        public ActionResult Update(int id, string firstName, string lastName, string birthDate, string hireDate, string email, string phone)
        {
            string query = "UPDATE Artists SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, HireDate = @HireDate, Email = @Email, Phone = @Phone where ArtistId = @ArtistId";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@FirstName", firstName);
            sqlparams[1] = new SqlParameter("@LastName", lastName);
            sqlparams[2] = new SqlParameter("@BirthDate", birthDate);
            sqlparams[3] = new SqlParameter("@HireDate", hireDate);
            sqlparams[4] = new SqlParameter("@Email", email);
            sqlparams[5] = new SqlParameter("@Phone", phone);
            sqlparams[6] = new SqlParameter("@ArtistId", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //go back to the list of artists
            return RedirectToAction("List");

        }
    }
}