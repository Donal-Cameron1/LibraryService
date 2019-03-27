using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public enum BookGenre
    {
        Action,Crime, Fantasy, Horror, Poetry, Romance, Thriller
    }
    public class Book: LibraryItem
    {
        [Key]
        public int id { get; set; }
        public string Author { get; set; }  
        public BookGenre BookGenre { get; set; }
        public int Pages { get; set; }  
        
        public virtual Library Library { get; set; }
        public virtual User User { get; set; }

    }

}