using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryService.Models;

namespace LibraryService.DAL
{
    public class LibraryInitialiser : DropCreateDatabaseAlways<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            var Libraries = new List<Library>
            {
                 new Library {Address ="London Road", Housenumber="12", PostCode="S2 4NF", Name="Highfield Library", Capacity=12345, TelephoneNumber="0114 123457", OpeningHours="Monday - Friday 8am-9pm", Coord="53.366833, -1.474933"}, 
                 new Library {Address="Hemper Lane", Housenumber="160", PostCode="S8 7FE", Name="Greenhill Library", Capacity=49568, TelephoneNumber="0114 987654", OpeningHours="Monday - Friday 8am-9pm", Coord="53.326512, -1.488615"},
                 new Library {Address="Chesterfield Road", Housenumber="900", PostCode="S8 0SH", Name="Woodseats Library", Capacity=304985, TelephoneNumber="0114 842394", OpeningHours="Monday - Friday 8am-9pm", Coord="53.340000, -1.480345"}
             };
            
            Libraries.ForEach(s => context.Libraries.Add(s));
            context.SaveChanges();

            var Books = new List<Book>
            {
                new Book {Title="Harry Potter", Author="J.K. Rowling", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=200, AgeRestriction=AgeRestriction._18, PurchaseValue=9.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Reserved, UserId="3", DateAdded=new DateTime(2019,4,6), PublishedAt=new DateTime(2006,12,6)},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=AgeRestriction.PG, PurchaseValue=11.99M, Type=Models.Type.Book, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,3,22), PublishedAt=new DateTime(2007,4,5)},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=AgeRestriction._12, PurchaseValue=11.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,2,14), PublishedAt=new DateTime(2014,3,4)},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=AgeRestriction._15, PurchaseValue=11.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,2,23), PublishedAt=new DateTime(2008,3,12)}
            };
            Books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            var DVD = new List<DVD>
            {
                new DVD {Title="Harry Potter and the Philosopher's Stone", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=152, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Reserved, DateAdded=new DateTime(2019,1,12),PublishedAt=new DateTime(2007,4,5)},
                new DVD {Title="Harry Potter and the Chamber of Secrets", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=161, AgeRestriction=AgeRestriction._12, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,4,3), PublishedAt=new DateTime(2007,4,5)},
                new DVD {Title="Forrest Gumpp", Director="Robert Zemeckis", Publisher="Paramount Pictures", DVDGenre=DVDGenre.Drama, Duration=142, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=new DateTime(2007,4,5)}
            };
            DVD.ForEach(s => context.DVD.Add(s));
            context.SaveChanges();  
            

            //context.Users.Add(new User { UserId = "sjdfle342", BookmarkedBooks = new List<Book> { new Book() } , ReservedBooks = new List<Book> { new Book() } , LoanedBooks = new List<Book> { new Book() } } );
            //context.SaveChanges();
      
        }

    }
}