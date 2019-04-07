using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public enum Genre
    {
        Action, Crime, Comedy, Drama, Fantasy, Horror, Poetry, Romance, Thriller
    }

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int AgeRestriction { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> PublishedAt { get; set; }      
        public Status Status { get; set; }
        public Type Type { get; set; }
        public Genre Genre { get; set; }
        public float PurchaseValue { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> DateAdded { get; set; }
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> ReturnDate { get; set; }
        public ICollection<User> BookmarkedBy { get; set; } = new List<User>();



    }
}