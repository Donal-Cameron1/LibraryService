using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public enum BookGenre
    {
        Action,Crime, Fantasy, Horror, Poetry, Romance, Thriller
    }
    public class Book: LibraryItem
    {
        public string Author { get; set; }  
        public BookGenre BookGenre { get; set; }
        public int Pages { get; set; }
        //public ICollection<User> BookmarkedBy { get; set; } = new List<User>();
        public User ReservedBy { get; set; }
        public User LoanedBy { get; set; }       
        
        public virtual Library Library { get; set; }
        //public ICollection<string> User { get; set; }

    }

}