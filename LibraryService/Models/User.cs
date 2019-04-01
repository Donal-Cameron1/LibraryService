using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Nullable<DateTime> MemberSince { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public ICollection<Book> BookmarkedBooks { get; set; } 
        //public ICollection<Book> ReservedBooks { get; set; } 
        //ublic ICollection<Book> LoanedBooks { get; set; } 
    }
}