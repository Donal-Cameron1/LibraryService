﻿using LibraryService.Data.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public enum Genre
    {
        Action, Crime, Comedy, Drama, Fantasy, Horror, Poetry, Romance, Thriller
    }

    public enum Status
    {
        Available, Reserved, Loaned
    }

    public enum Type
    {
        Book, DVD
    }

    public enum AgeRestriction
    {
        U = 1,
        PG = 2,
        [Display(Name = "12")]
        _12 = 12,
        [Display(Name = "15")]
        _15 = 15,
        [Display(Name = "18")]
        _18 = 18
    }

    public class LibraryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        [Display(Name = "Age Restriction")]
        public AgeRestriction AgeRestriction { get; set; }

        [Required]
        [Display(Name = "Published at")]
        [ValidationYearInPast(ErrorMessage = "Published at must be in the past")]
        public int PublishedAt { get; set; }

        public Status Status { get; set; }

        public Type Type { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Purchase Value")]
        [Range(1, 200)]
        [DataType(DataType.Currency)]
        public decimal PurchaseValue { get; set; }

        [Display(Name = "Date added")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> DateAdded { get; set; }

        [Required]
        [Display(Name = "Library")]
        public int LibraryId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Return Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> ReturnDate { get; set; }

        [Display(Name = "Reserved Until")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> ReservedUntil { get; set; }

        public ICollection<User> BookmarkedBy { get; set; } = new List<User>();
        public User ReservedBy { get; set; }
        public User LoanedBy { get; set; }

    }
}

