using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public class Book: LibraryItem
    {
       
        public int id { get; set; }
        [Key]
        public string Author { get; set; }       
        public string Publisher { get; set; }                
        public int Pages { get; set; }                   
        public virtual Library Library { get; set; }
        public virtual User User { get; set; }

    }

}