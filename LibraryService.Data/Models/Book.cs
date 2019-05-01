using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public enum BookGenre
    {
        Action, Crime, Fantasy, Horror, Poetry, Romance, Thriller
    }
    public class Book : LibraryItem
    {
        [Required]
        [RegularExpression("[A-Z](.*)", ErrorMessage = "Author has to begin with a capital letter")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public BookGenre BookGenre { get; set; }

        [Required]
        [Range(5, 5000)]
        public int Pages { get; set; }

    }
}