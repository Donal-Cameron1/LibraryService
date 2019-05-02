using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Data.Utils
{

    public class ValidationYearInPast : RangeAttribute
    {
        public ValidationYearInPast()
            : base(1500, DateTime.Today.Year) { }
    }

}
