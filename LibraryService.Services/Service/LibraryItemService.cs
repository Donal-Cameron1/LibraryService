using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace LibraryService.Services.Service
{
    public class LibraryItemService : ILibraryItemService
    {
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;
        private IBookDAO _bookDAO;
        private IDVDDAO _dvdDAO;
        private ILibraryItemDAO _libraryItemDAO;

        public LibraryItemService()
        {
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
            _bookDAO = new BookDAO();
            _dvdDAO = new DVDDAO();
            _libraryItemDAO = new LibraryItemDAO();
        }

        public LibraryItem GetLibraryItem(int id)
        {
            return _libraryItemDAO.GetLibaryItem(id);
        }

        //Returns a list of all library items
        public IList<LibraryItem> GetLibraryItems()
        {
            IList<LibraryItem> libraryItems = new List<LibraryItem>();
            libraryItems = _libraryItemDAO.GetLibraryItems();

            return libraryItems;
        }

        //Returns a list of reserved library items for the selected user in Staff/Admin View
        public IList<LibraryItem> GetReservedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> reservedLibraryItems = new List<LibraryItem>();
            reservedLibraryItems = _libraryItemDAO.GetReservedLibraryItemsOfUser(id);

            return reservedLibraryItems;
        }
        
        //Returns a list of loaned library items for the selected user in Staff/Admin View
        public IList<LibraryItem> GetLoanedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> loanedLibraryItems = new List<LibraryItem>();
            loanedLibraryItems = _libraryItemDAO.GetLoanedLibraryItems();

            return loanedLibraryItems;
        }
       
        //Returns list of all loaned library items for the Staff/Admin View
        public IList<LibraryItem> GetLoanedLibraryItems()
        {
            IList<LibraryItem> loanedLibraryItems = new List<LibraryItem>();
            loanedLibraryItems = _libraryItemDAO.GetLoanedLibraryItems();
            return loanedLibraryItems;
        }
        
        //Returns list of overdue items for the Staff/Admin View
        public IList<LibraryItem> GetOverdueLibraryItems()
        {
            IList<LibraryItem> overdueLibraryItems = new List<LibraryItem>();
            overdueLibraryItems = _libraryItemDAO.GetOverdueLibraryItems();

            return overdueLibraryItems;
        }

        public void LoanLibraryItem(int id)
        {
            _libraryItemDAO.LoanLibraryItem(id);
        }

        public void ExtendLoan(int id)
        {
            _libraryItemDAO.ExtendLoan(id);
        }

        public void ReturnLibraryItem(int id)
        {
            _libraryItemDAO.ReturnLibraryItem(id);
        }


        //Casts the DVDs to a library item list, so that genre gets displayed correctly
        public static List<LibraryItem> CastDVDsToLibraryItems(IList<DVD> dvdquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (DVD dvd in dvdquery.ToList())
            {
                LibraryItem item = dvd;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), dvd.DVDGenre.ToString());
                items.Add(item);
            }
            return items;
        }
        
        //Casts the books to a library item list, so that genre gets displayed correctly
        public static List<LibraryItem> CastBooksToLibraryItems(IList<Book> bookquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (Book book in bookquery.ToList())
            {
                LibraryItem item = book;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), book.BookGenre.ToString());
                items.Add(item);
            }
            return items;
        }

        public static List<LibraryItem> AssignCorrectGenre(IList<LibraryItem> libraryItems)
        {
            List<Book> books = libraryItems.Where(i => i.Type == Models.Type.Book).Cast<Book>().ToList();
            List<DVD> dvds = libraryItems.Where(i => i.Type == Models.Type.DVD).Cast<DVD>().ToList();
            return new List<LibraryItem>().Concat(CastBooksToLibraryItems(books)).Concat(CastDVDsToLibraryItems(dvds)).ToList();
        }


        //this gets executed daily, checks if any reservedUntil date is in the past and then deletes the reservation
        public void UpdateStatus()
        {
            foreach (User user in _userDAO.GetUsers())
            {
                if (user.ReservedLibraryItems != null)
                {
                    foreach (LibraryItem item in user.ReservedLibraryItems.ToList())
                    {
                        if (item.ReservedUntil != null && (item.ReservedUntil.Value.AddDays(1).CompareTo(DateTime.Today) <= 0))
                        {
                            item.ReservedUntil = null;
                            item.Status = Status.Available;
                            _libraryItemDAO.UpdateLibraryItem(item);
                            _libraryItemDAO.DeleteReservation(item.id, user.UserId);
                        }
                    }
                }
            }
        }

        
        //this gets executed daily, checks if there are any overdue books and sends an email to the associated user
        public void SendOverdueMail()
        {
            SmtpClient client = new SmtpClient("smtp.googlemail.com");
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential("LibraryServiceSheffield@gmail.com", "LibraryService123");

            foreach (User user in _userDAO.GetUsers())
            {
                foreach (LibraryItem libraryItem in user.LoanedLibraryItems)
                {
                    if (libraryItem.ReturnDate.Value.AddDays(1).CompareTo(DateTime.Today) <= 0)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("LibraryServiceSheffield@gmail.com");
                        mailMessage.To.Add(user.UserName);
                        mailMessage.Subject = "Overdue Item";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = "Dear Customer.  <br /> <br /> The " + libraryItem.Type + " " + libraryItem.Title + " is overdue.  <br /> Please bring it back to the Library as soon as possible. <br /> <br />  Regards, <br /> Library Service Sheffield";

                        client.Send(mailMessage);
                    }

                }
            }
        }

        public IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString)
        {
            return _libraryItemDAO.TextSearch(loanedItems, searchString);
        }

        public void BookmarkLibraryItem(int id, string currentUserId)
        {
            _libraryItemDAO.BookmarkLibraryItem(id, currentUserId);
        }

        public void DeleteBookmark(int id, string currentUserId)
        {
            _libraryItemDAO.DeleteBookmark(id, currentUserId);
        }

        public void ReserveLibraryItem(int id, string currentUserId)
        {
            _libraryItemDAO.ReserveLibraryItem(id, currentUserId);
        }

        public void DeleteReservation(int id, string currentUserId)
        {
            _libraryItemDAO.DeleteReservation(id, currentUserId);
        }
        
    }
}
