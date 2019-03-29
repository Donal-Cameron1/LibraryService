using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public class Library
    {
        public int LibraryId { get; set; }
        [Key]
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Phone { get; set; }
        public string OpenHours { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}