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
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services.Service
{
    public class LibraryItemService : ILibraryItemService
    {
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;
        private IBookDAO _bookDAO;
        private IDVDDAO _dvdDAO;

        public LibraryItemService()
        {
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
            _bookDAO = new BookDAO();
            _dvdDAO = new DVDDAO();
        }

        public void UpdateStatus()
        {
            foreach(User user in _userDAO.GetUsers())
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

        public void SendOverdueMail()
        {
            SmtpClient client = new SmtpClient("smtp.googlemail.com");
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential("LibraryServiceSheffield@gmail.com", "LibraryService123");

            foreach (User user in _userDAO.GetUsers())
            {
                foreach(KeyValuePair<Book, DateTime> book_duedate in user.LoanedBooks)
                {
                    if (book_duedate.Value.AddDays(1).CompareTo(DateTime.Today) <= 0)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress("LibraryServiceSheffield@gmail.com");
                        mailMessage.To.Add(user.UserName);
                        mailMessage.Subject = "Hello There";
                        mailMessage.Body = "Hello my friend!";

                        client.Send(mailMessage);
                    }

                }

                foreach (KeyValuePair<DVD, DateTime> dvd_overdue in user.LoanedDVDs)
                {
                    if (dvd_overdue.Value.AddDays(1).CompareTo(DateTime.Today) <= 0)
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
    }
}
