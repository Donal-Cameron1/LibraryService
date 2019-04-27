using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
}