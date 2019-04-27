using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public enum DVDGenre
    {
        Action, Crime, Comedy, Drama, Fantasy, Horror, Romance, Thriller
    }

    public class DVD : LibraryItem
    {
        [Required]
        [RegularExpression("[A-Z](.*)", ErrorMessage = "Author has to begin with a capital letter")]
        public string Director { get; set; }

        [Required]
        [Range(2, 500)]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public DVDGenre DVDGenre { get; set; }

        public virtual Library Library { get; set; }
        public virtual User User { get; set; }
    }
}