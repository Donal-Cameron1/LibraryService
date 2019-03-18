using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryService.Models;

namespace LibraryService.DAL
{
    public class LibraryInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            var Libraries = new List<Library>
            {
                new Library {LibraryId=1 , Address="London Road", PostCode="S2 4NF", Name="Highfield Library", Capacity=12345},
                   new Library {LibraryId=2 , Address="160 Hemper Lane", PostCode="S8 7FE", Name="Greenhill Library", Capacity=49568},
                   new Library {LibraryId=3, Address="900 Chesterfield Road", PostCode="S8 0SH", Name="Woodseats Library", Capacity=304985}
            };
            
            Libraries.ForEach(s => context.Libraries.Add(s));
            context.SaveChanges();

            var Books = new List<Book>
            {
                new Book {CatalogueId=1 , Title="Harry Potter", Author="Jk Rowling", Publisher="Penguin", LibraryId=2, Status="Reserved", UserId= 3 , AgeRestriction= "7+", Pages= 200, PurchaseValue= 9},
                 new Book {CatalogueId=2 , Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", LibraryId=3, Status="Reserved", UserId= 2 , AgeRestriction= "5+", Pages= 150, PurchaseValue= 11}
            };
            Books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();
        }

    }
}