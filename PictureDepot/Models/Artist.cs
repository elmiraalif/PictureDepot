using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using PictureDepot.Controllers;

namespace PictureDepot.Models
{
    public class Artist
        /* An Artist has the properties shown below which represent the column names of the database table of Artist
             id
             firstname
             lastname
             birthdate
             hiredate
             email
             phone
         */
    {
        [Key]
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }



    }
}