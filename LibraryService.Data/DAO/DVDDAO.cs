using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.DAO
{
    public class DVDDAO : IDVDDAO
    {
        private LibraryContext db = new LibraryContext();

        public void CreateDVD(DVD dvd)
        {
            db.DVD.Add(dvd);
            db.SaveChanges();
        }

        public void DeleteDVD(DVD dvd)
        {
            db.DVD.Attach(dvd);
            db.DVD.Remove(dvd);
            db.SaveChanges();
        }

        public IList<DVD> DVDGenreFilter(IList<DVD> query, string genre)
        {
            return query.Where(d => d.DVDGenre.ToString().Equals(genre)).ToList<DVD>();
        }

        public IList<DVD> DVDStatusFilter(IList<DVD> query, string status)
        {
            return query.Where(d => d.Status.ToString().Equals(status)).ToList<DVD>();
        }

        public IList<DVD> DVDTextSearch(IList<DVD> query, string searchString)
        {
            return query.Where(d => d.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                 || d.Director.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<DVD>();
        }

        public IList<DVD> DVDTypeFilter(IList<DVD> query, string type)
        {
            return query.Where(d => d.Type.ToString().Equals(type)).ToList<DVD>();
        }

        public void EditDVD(DVD dvd)
        {
            db.DVD.Attach(dvd);
            db.Entry(dvd).State = EntityState.Modified;
            db.SaveChanges();
        }

        public DVD GetDVD(int id)
        {
            return db.DVD.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }

        public static DVD GetDVDWithTracking(LibraryContext context, int id)
        {
            return context.DVD.Where(b => b.id == id).FirstOrDefault();
        }

        public IList<DVD> GetDVDs()
        {
            IQueryable<DVD> dvdquery;
            dvdquery = from d in db.DVD select d;
            return dvdquery.AsNoTracking().ToList<DVD>();
        }

        public IList<DVD> GetNewDVDs()
        {
            var baselineDate = DateTime.Now.AddDays(-7);
            IList<DVD> newDVDs = db.DVD.AsNoTracking().Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList();
            return newDVDs;
        }

        public void BookmarkDVD(int id, string currentUserId)
        {
            DVD dvd = GetDVDWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Add(dvd);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteBookmark(int id, string currentUserId)
        {
            DVD dvd = GetDVDWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Remove(dvd);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ReserveDVD(int id, string currentUserId)
        {
            DVD dvd = GetDVDWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.ReservedLibraryItems.Add(dvd);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteReservation(int id, string currentUserId)
        {
            DVD dvd = GetDVDWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            dvd.Status = Status.Available;
            user.ReservedLibraryItems.Remove(dvd);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        
         public void LoanDVD (int id, string currentUserId)

        {
            DVD dvd = GetDVDWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.LoanedLibraryItems.Add(dvd);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
