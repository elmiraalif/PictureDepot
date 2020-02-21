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
            string query = "INSERT INTO Artists(FirstName, LastName, BirthDate, HireDate, Email, Phone) VALUES (@FirstName, @LastName, @BirthDate, @HireDate, @Email, @Phone)";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@FirstName", firstName);
            sqlparams[1] = new SqlParameter("@LastName", lastName);
            sqlparams[2] = new SqlParameter("@BirthDate", birthDate);
            sqlparams[3] = new SqlParameter("@HireDate", hireDate);
            sqlparams[4] = new SqlParameter("@Email", email);
            sqlparams[5] = new SqlParameter("@Phone", phone);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        //This is the get request
        public ActionResult Delete()
        {
            return View();
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

    //the Get request fo rshowing the specific Artist
        public ActionResult Show(int id)
        {
            string query = "SELECT * FROM Artists WHERE ArtistId = @ArtistId";
            var parameter = new SqlParameter("@ArtistId", id);
            Artist selectedArtist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedArtist);
        }

        //Get request of Update => takes the user to the page of Update
        public ActionResult Update(int id)
        {
            string query = "SELECT * FROM Artists where ArtistId = @ArtistId";
            var parameter = new SqlParameter("@ArtistId", id);
            Artist selectedArtist = db.Artists.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedArtist);
        }

        //post request of Update to make changes 
        [HttpPost]
        public ActionResult Update(string firstName, string lastName, string birthDate, string hireDate, string email, string phone)
        {
            string query = "UPDATE Artists SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, HireDate = @HireDate, Email = @Email, Phone = @Phone where ArtistId = @ArtistId";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@FirstName", firstName);
            sqlparams[1] = new SqlParameter("@LastName", lastName);
            sqlparams[2] = new SqlParameter("@BirthDate", birthDate);
            sqlparams[3] = new SqlParameter("@HireDate", hireDate);
            sqlparams[4] = new SqlParameter("@Email", email);
            sqlparams[5] = new SqlParameter("@Phone", phone);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            //go back to the list of artists
            return RedirectToAction("List");

        }
    }
}