using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}