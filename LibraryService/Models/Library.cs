using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public class Library
    {
        [Key]
        public string LibraryId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Name { get; set; }
        public string OpeningHours { get; set; }
        public string TelephoneNumber { get; set; }
        public int Capacity { get; set; }
        

        public virtual ICollection<Book> Books { get; set; }

    }
}