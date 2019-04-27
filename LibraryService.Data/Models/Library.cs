using System.Collections.Generic;
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
        public string TelephoneNumber { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        [Range(100, 5000000)]
        public int Capacity { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}