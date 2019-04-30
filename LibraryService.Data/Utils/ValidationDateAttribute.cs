using System;
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

}