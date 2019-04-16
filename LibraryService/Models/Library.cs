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

        [Required]
        [RegularExpression("[A-Z](.*)", ErrorMessage = "Address has to begin with a capital letter")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("(\\d+\\w|\\d+)")]
        public string Housenumber { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression("([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})", ErrorMessage = "Not a valid postal code")]
        public string PostCode { get; set; }

        [Required]
        [RegularExpression("[A-Z](.*)", ErrorMessage = "Library Name has to begin with a capital letter")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opening Hours")]
        public string OpeningHours { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{6})$", ErrorMessage = "Not a valid phone number")]
        public string TelephoneNumber { get; set; }

        [Required]
        public string Coord { get; set; }

        [Required]
        [Range(100,5000000)]
        public int Capacity { get; set; }
         

        public virtual ICollection<Book> Books { get; set; }

    }
}