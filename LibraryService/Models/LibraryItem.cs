using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public abstract class LibraryItem
    {
   
    
        public string Title { get; set; }
        public string Genre { get; set; }
        public int LibraryId { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string AgeRestriction { get; set; }
        public int PurchaseValue { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public Nullable<DateTime> DateAdded { get; set; }

    }
}