using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public class Book
    {
        public int CatalogueId { get; set; }
        [Key]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int LibraryId { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string AgeRestriction { get; set; }
        public int Pages { get; set; }
        public int PurchaseValue { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public Nullable<DateTime> DateAdded { get; set; }

        public virtual Library Library { get; set; }
        public virtual User User { get; set; }

    }

}