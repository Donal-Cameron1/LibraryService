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
                new Book {id=1 , Title="Harry Potter", Author="J.K. Rowling", Publisher="Penguin", BookGenre=BookGenre.Fantasy, LibraryId=2, Status=Status.Reserved, UserId=3 , AgeRestriction=7, Pages=200, PurchaseValue=9, Type=Models.Type.Book},
                new Book {id=2 , Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, LibraryId=3, Status=Status.Available, UserId=2 , AgeRestriction=5, Pages=150, PurchaseValue= 11, Type=Models.Type.Book}
            };
            Books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            var DVD = new List<DVD>
            {
                new DVD {id=1, Title="Harry Potter", Director="", Publisher="", LibraryId=1, Status=Status.Available, AgeRestriction=12, Duration=120, PurchaseValue=12, Type=Models.Type.DVD}
            };
            DVD.ForEach(s => context.DVD.Add(s));
            context.SaveChanges();
        }

    }
}