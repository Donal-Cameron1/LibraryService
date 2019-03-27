using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LibraryService.Models
{
    public enum DVDGenre
    {
        Action, Crime, Comedy, Drama, Fantasy, Horror, Romance, Thriller
    }

    public class DVD : LibraryItem
    {
        [Key]
        public int id { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public DVDGenre DVDGenre { get; set; }

        public virtual Library Library { get; set; }
        public virtual User User { get; set; }
    }
}