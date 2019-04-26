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

        public IList<string> Roles { get; set; }

        public Nullable<DateTime> MemberSince { get; set; }

        public Nullable<DateTime> DateOfBirth { get; set; }

        public virtual DateTime? LastLogin { get; set; }

        public ICollection<LibraryItem> BookmarkedLibraryItems { get; set; } = new List<LibraryItem>();

        [InverseProperty("ReservedBy")]
        public ICollection<LibraryItem> ReservedLibraryItems { get; set; }

        [InverseProperty("LoanedBy")]
        public Dictionary<Book, DateTime> LoanedBooks { get; set; }

        [InverseProperty("LoanedBy")]
        public Dictionary<DVD, DateTime> LoanedDVDs { get; set; }
   
    }
}