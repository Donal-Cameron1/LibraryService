using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LibraryService.Models;

namespace LibraryService.DAL
{
    public class LibraryInitialiser : DropCreateDatabaseIfModelChanges<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            var Libraries = new List<Library>
            {
                new Library {LibraryId="1" , Address="London Road", PostCode="S2 4NF", Name="Highfield Library", Capacity=12345, TelephoneNumber="0114 1234567", OpeningHours="Monday - Friday 8am-9pm", Coord="53.366833, -1.474933"}, 
                new Library {LibraryId="2" , Address="160 Hemper Lane", PostCode="S8 7FE", Name="Greenhill Library", Capacity=49568, TelephoneNumber="0114 987654", OpeningHours="Monday - Friday 8am-9pm", Coord="53.326512, -1.488615"},
                new Library {LibraryId="3" , Address="900 Chesterfield Road", PostCode="S8 0SH", Name="Woodseats Library", Capacity=304985, TelephoneNumber="0114 842394", OpeningHours="Monday - Friday 8am-9pm", Coord="53.340000, -1.480345"}
            };
            
            Libraries.ForEach(s => context.Libraries.Add(s));
            context.SaveChanges();

            var Books = new List<Book>
            {
                new Book {Title="Harry Potter", Author="J.K. Rowling", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=200, AgeRestriction=7, PurchaseValue=9, Type=Models.Type.Book, LibraryId=2, Status=Status.Reserved, UserId=3},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=5, PurchaseValue=11, Type=Models.Type.Book, LibraryId=3, Status=Status.Available},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=5, PurchaseValue=11, Type=Models.Type.Book, LibraryId=2, Status=Status.Available},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=5, PurchaseValue=11, Type=Models.Type.Book, LibraryId=2, Status=Status.Available}
            };
            Books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            var DVD = new List<DVD>
            {
                new DVD {Title="Harry Potter and the Philosopher's Stone", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=152, AgeRestriction=12, PurchaseValue=7.99f, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available},
                new DVD {Title="Harry Potter and the Chamber of Secrets", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=161, AgeRestriction=12, PurchaseValue=7.99f, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available},
                new DVD {Title="Forrest Gumpp", Director="Robert Zemeckis", Publisher="Paramount Pictures", DVDGenre=DVDGenre.Drama, Duration=142, AgeRestriction=12, PurchaseValue=8.99f, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available}
            };
            DVD.ForEach(s => context.DVD.Add(s));
            context.SaveChanges();  
            

            //context.Users.Add(new User { UserId = "sjdfle342", BookmarkedBooks = new List<Book> { new Book() } , ReservedBooks = new List<Book> { new Book() } , LoanedBooks = new List<Book> { new Book() } } );
            //context.SaveChanges();
      
        }

    }
}