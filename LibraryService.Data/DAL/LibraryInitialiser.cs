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
                 new Library {Address ="London Road", Housenumber="12", PostCode="S2 4NF", Name="Highfield Library", Capacity=12345, TelephoneNumber="0114 123457", OpeningHours="Monday - Friday 8am-9pm", Coord="53.366833, -1.474933"}, 
                 new Library {Address="Hemper Lane", Housenumber="160", PostCode="S8 7FE", Name="Greenhill Library", Capacity=49568, TelephoneNumber="0114 987654", OpeningHours="Monday - Friday 8am-9pm", Coord="53.326512, -1.488615"},
                 new Library {Address="Chesterfield Road", Housenumber="900", PostCode="S8 0SH", Name="Woodseats Library", Capacity=304985, TelephoneNumber="0114 842394", OpeningHours="Monday - Friday 8am-9pm", Coord="53.340000, -1.480345"},
                 new Library {Address="Test", Housenumber="900", PostCode="S8 0SH", Name="Test Library", Capacity=304985, TelephoneNumber="0114 842394", OpeningHours="Monday - Friday 8am-9pm", Coord="53.340000, -1.480345"}
             };
            
            Libraries.ForEach(s => context.Libraries.Add(s));
            context.SaveChanges();

            var Books = new List<Book>
            {
                new Book {Title="Harry Potter", Author="J.K. Rowling", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=200, AgeRestriction=AgeRestriction._18, PurchaseValue=9.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Reserved, UserId="3", DateAdded=new DateTime(2019,4,6), PublishedAt=2006},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=AgeRestriction.PG, PurchaseValue=11.99M, Type=Models.Type.Book, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,3,22), PublishedAt=2007},
                new Book {Title="A Brief History of Time", Author="Stephen Hawking", Publisher="Penguin", BookGenre=BookGenre.Action, Pages=400, AgeRestriction=AgeRestriction._12, PurchaseValue=5.99M, Type=Models.Type.Book, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2015,3,12), PublishedAt=1999},
                new Book {Title="An Action Book", Author="Brian SMith", Publisher="Random House", BookGenre=BookGenre.Action, Pages=550, AgeRestriction=AgeRestriction._18, PurchaseValue=2.99M, Type=Models.Type.Book, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2012,1,10), PublishedAt=1997},
                new Book {Title="Gardeing is Cool", Author="Janet McSmedley", Publisher="Smedley Books", BookGenre=BookGenre.Action, Pages=50, AgeRestriction=AgeRestriction.U, PurchaseValue=23.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2004,2,14), PublishedAt=1979},
                new Book {Title="Gritty Crime", Author="Robert McDude", Publisher="Dude Publishing", BookGenre=BookGenre.Thriller, Pages=350, AgeRestriction=AgeRestriction.PG, PurchaseValue=1.99M, Type=Models.Type.Book, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2012,1,12), PublishedAt=2002},
                new Book {Title="A book of thinghs", Author="Robert McDude", Publisher="Dude Publishing", BookGenre=BookGenre.Poetry, Pages=450, AgeRestriction=AgeRestriction.U, PurchaseValue=2.99M, Type=Models.Type.Book, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2014,2,23), PublishedAt=2003},
                new Book {Title="The Lion the whitch and the wardrobe", Author="Lewis Caroll", Publisher="Penguin", BookGenre=BookGenre.Fantasy, Pages=150, AgeRestriction=AgeRestriction._15, PurchaseValue=11.99M, Type=Models.Type.Book, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,2,23), PublishedAt=2008}


            };
            Books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();

            var DVD = new List<DVD>
            {
                new DVD {Title="Harry Potter and the Philosopher's Stone", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=152, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Reserved, DateAdded=new DateTime(2019,1,12),PublishedAt=2007},
                new DVD {Title="Harry Potter and the Chamber of Secrets", Director="Chris Columbus", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Fantasy, Duration=161, AgeRestriction=AgeRestriction._12, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,4,3), PublishedAt=2007},
                new DVD {Title="Forrest Gumpp", Director="Robert Zemeckis", Publisher="Paramount Pictures", DVDGenre=DVDGenre.Drama, Duration=142, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2007},
                new DVD {Title="Star Wars:Episode IV: A new hope", Director="George Lucas", Publisher="LucasFilms Ltd.", DVDGenre=DVDGenre.Fantasy, Duration=152, AgeRestriction=AgeRestriction.PG, PurchaseValue=9.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1977},
                new DVD {Title="Raiders of the lost ark", Director="Steven Spielberg", Publisher="LucasFilm Ltd. ", DVDGenre=DVDGenre.Action, Duration=115, AgeRestriction=AgeRestriction._12, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=1982},       
                new DVD {Title="Indiana Jones and the kingdom of the crystal skull", Director="Steven Spielberg", Publisher="LucasFilm Ltd.", DVDGenre=DVDGenre.Action, Duration=122, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2008},
                new DVD {Title="Star Wars:Episode V: The empire strikes back", Director="Irvin Kershner", Publisher="LucasFilm Ltd.", DVDGenre=DVDGenre.Fantasy, Duration=124, AgeRestriction=AgeRestriction.PG, PurchaseValue=9.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1980},
                new DVD {Title="Indiana Jones and the temple of doom", Director="Steven Spielberg", Publisher="LucasFilm Ltd. ", DVDGenre=DVDGenre.Action, Duration=118, AgeRestriction=AgeRestriction.PG, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=1984},
                new DVD {Title="Indiana Jones and the last crusade", Director="Steven Spielberg", Publisher="LucasFilm Ltd.", DVDGenre=DVDGenre.Action, Duration=127, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=1989},
                new DVD {Title="Back to the future", Director="Robert Zemeckis", Publisher="Universal Pictures", DVDGenre=DVDGenre.Fantasy, Duration=116, AgeRestriction=AgeRestriction.PG, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1985},    
                new DVD {Title="Interstellar", Director="Christopher Nolan", Publisher="Legacy Pictures", DVDGenre=DVDGenre.Drama, Duration=169, AgeRestriction=AgeRestriction._12, PurchaseValue=9.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=2014},
                new DVD {Title="Dunkirk", Director="Christopher Nolan", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Drama, Duration=106, AgeRestriction=AgeRestriction._12, PurchaseValue=11.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2017},
                new DVD {Title="The Martian", Director="Ridley Scott", Publisher="20th Century Fox", DVDGenre=DVDGenre.Drama, Duration=144, AgeRestriction=AgeRestriction._12, PurchaseValue=5.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=2015},
                new DVD {Title="Gravity", Director="Alfonso Cuaron", Publisher="Warner Bros. Pictures", DVDGenre=DVDGenre.Drama, Duration=91, AgeRestriction=AgeRestriction._12, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=2013},
                new DVD {Title="Alien", Director="Ridley Scott", Publisher="20th Century Fox", DVDGenre=DVDGenre.Horror, Duration=116, AgeRestriction=AgeRestriction._18, PurchaseValue=4.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=1979},
                new DVD {Title="Aliens", Director="James Cameron", Publisher="20th Century Fox", DVDGenre=DVDGenre.Horror, Duration=137, AgeRestriction=AgeRestriction._15, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1986},
                new DVD {Title="Avatar", Director="James Cameron", Publisher=" 20th Century Fox", DVDGenre=DVDGenre.Fantasy, Duration=162, AgeRestriction=AgeRestriction._12, PurchaseValue=2.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=2009},
                new DVD {Title="Titanic", Director="James Cameron", Publisher="Paramount Pictures", DVDGenre=DVDGenre.Drama, Duration=194, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=1997},
                new DVD {Title="Lion king", Director="Roger Allers", Publisher="Walt Disney Pictures", DVDGenre=DVDGenre.Fantasy, Duration=84, AgeRestriction=AgeRestriction.U, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1994},
                new DVD {Title="Toy story", Director="John Lasseter", Publisher="Walt Disney Pictures" , DVDGenre=DVDGenre.Comedy, Duration=81, AgeRestriction=AgeRestriction.PG, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=1995},
                new DVD {Title="Monsters Inc", Director="Pete Docotr", Publisher="Walt Disney Pictures", DVDGenre=DVDGenre.Comedy, Duration=92, AgeRestriction=AgeRestriction.U, PurchaseValue=5.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2002},
                new DVD {Title="Shaun of the dead", Director="Edgar Wright", Publisher="Universal Pictures", DVDGenre=DVDGenre.Comedy, Duration=99, AgeRestriction=AgeRestriction._15, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=2004},
                new DVD {Title="Hot fuzz", Director="Edgar Wright", Publisher="Universal Pictures", DVDGenre=DVDGenre.Comedy, Duration=121, AgeRestriction=AgeRestriction._15, PurchaseValue=6.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=2007},
                new DVD {Title="Baby driver", Director="Edgar Wright", Publisher="Working Title Films ", DVDGenre=DVDGenre.Action, Duration=112, AgeRestriction=AgeRestriction._15, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2017},
                new DVD {Title="Pulp fiction", Director="Quentin Tarantino", Publisher="Miramax Films", DVDGenre=DVDGenre.Crime, Duration=154, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=2015},
                new DVD {Title="Fight club", Director="David Fincher", Publisher="20th Century Fox", DVDGenre=DVDGenre.Thriller, Duration=139, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=1999},
                new DVD {Title="Gone girl", Director="David Fincher", Publisher="20th Century Fox", DVDGenre=DVDGenre.Thriller, Duration=149, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=1, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2014},
                new DVD {Title="Seven", Director="David Fincher", Publisher="New Line Cinema", DVDGenre=DVDGenre.Crime, Duration=127, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,1,12),PublishedAt=1995},
                new DVD {Title="Scream", Director="Wes Craven", Publisher="Dimension Films", DVDGenre=DVDGenre.Horror, Duration=111, AgeRestriction=AgeRestriction._18, PurchaseValue=7.99M, Type=Models.Type.DVD, LibraryId=3, Status=Status.Available, DateAdded=new DateTime(2019,6,12), PublishedAt=1996},
                new DVD {Title="Avengers infinity war", Director="Joe Russo", Publisher="Marvel Studios", DVDGenre=DVDGenre.Action, Duration=149, AgeRestriction=AgeRestriction._12, PurchaseValue=8.99M, Type=Models.Type.DVD, LibraryId=2, Status=Status.Available, DateAdded= new DateTime(2019,3,31), PublishedAt=2018}
            };
            DVD.ForEach(s => context.DVD.Add(s));
            context.SaveChanges();      
        }
    }
}