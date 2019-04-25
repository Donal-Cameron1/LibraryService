using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryService.ViewModels
{
    public class BooksIndexVIewModel
    {
        public Book book { get; set; }
        public string libraryname { get; set; }
    }
}