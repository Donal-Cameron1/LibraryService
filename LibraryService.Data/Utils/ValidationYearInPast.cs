using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.Utils
{
   
    public class ValidationYearInPast : RangeAttribute
    {
        public ValidationYearInPast() 
            : base(1500, DateTime.Today.Year) { }
    }
    
}
