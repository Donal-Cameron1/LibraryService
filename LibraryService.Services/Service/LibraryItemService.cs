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

        public IList<LibraryItem> GetLibraryItems()
        {
            IList<LibraryItem> libraryItems = new List<LibraryItem>();
            libraryItems = _libraryItemDAO.GetLibraryItems();

            return libraryItems;
        }

        public IList<LibraryItem> GetReservedItems(string id)
        {
            IList<LibraryItem> reservedLibraryItems = new List<LibraryItem>();
            reservedLibraryItems = _libraryItemDAO.GetReservedLibraryItems(id);

            return reservedLibraryItems;
        }

        public IList<LibraryItem> GetLoanedLibraryItems()
        {
            IList<LibraryItem> loanedLibraryItems = new List<LibraryItem>();
            loanedLibraryItems = _libraryItemDAO.GetLoanedLibraryItems();

            return loanedLibraryItems;
        }

        public IList<LibraryItem> GetOverdueLibraryItems()
        {
            IList<LibraryItem> overdueItems = new List<LibraryItem>();
            overdueItems = _libraryItemDAO.GetOverdueLibraryItems();

            return overdueItems;
        }

        private static List<LibraryItem> CastDVDsToLibraryItems(IList<DVD> dvdquery)
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

        private static List<LibraryItem> CastBooksToLibraryItems(IList<Book> bookquery)
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
                            if (item.Type == Models.Type.Book)
                            {
                                _bookDAO.UpdateBook((Book)item);
                                _bookDAO.DeleteReservation(item.id, user.UserId);
                            }
                            else if (item.Type == Models.Type.DVD)
                            {
                                _dvdDAO.UpdateDVD((DVD)item);
                                _dvdDAO.DeleteReservation(item.id, user.UserId);
                            }
                        }
                    }
                }
            }
        }

        public void LoanLibraryItem(int id)
        {
            _libraryItemDAO.LoanLibraryItem(id);
        }

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
                        mailMessage.Subject = "Hello There";
                        mailMessage.Body = "Hello my friend!";

                        client.Send(mailMessage);
                    }

                }
            }
        }

        public IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString)
        {
            return _libraryItemDAO.TextSearch(loanedItems, searchString);
        }
    }
}
