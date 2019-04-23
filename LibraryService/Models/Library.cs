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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LibraryId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Name { get; set; }
        [Display(Name = "Opening Hours")]
        public string OpeningHours { get; set; }
        [Display(Name = "Phone Number")]
        public string TelephoneNumber { get; set; }
        public string Coord { get; set; }
        public int Capacity { get; set; }         

        public virtual ICollection<Book> Books { get; set; }

    }
}