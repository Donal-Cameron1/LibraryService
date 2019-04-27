﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.utils
{
    public class ValidationDateAttribute : RangeAttribute
    {
        public ValidationDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.AddYears(-100).ToShortDateString(),
                  DateTime.Now.ToShortDateString())
        { }
    }

    public class ValidationYearInPast : RangeAttribute
    {
        public ValidationYearInPast() : base(1500, DateTime.Today.Year) { }
    }

    public static class CustomRoles
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";
        public const string AdminOrStaff = Admin + "," + Staff;
    }
}