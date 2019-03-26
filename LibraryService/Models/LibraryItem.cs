using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public enum Status
    {
        Available, Reserved, Loaned
    }

    public enum Type
    {
        Book, DVD, CD
    }

  
 
    public class LibraryItem
    {
   
    
        public string Title { get; set; }
        public int LibraryId { get; set; }
        public int AgeRestriction { get; set; }
        public string Publisher { get; set; }
        public Nullable<DateTime> PublishedAt { get; set; }      
        public Status Status { get; set; }
        public Type Type { get; set; }
        public int PurchaseValue { get; set; }
        public Nullable<DateTime> DateAdded { get; set; }
        public int UserId { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }

    }
}