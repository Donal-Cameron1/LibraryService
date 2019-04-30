using LibraryService.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryService.Data.DAO
{
    public class DVDDAO : IDVDDAO
    {
        private LibraryContext db = new LibraryContext();

        //adds a new dvd to the database
        public void CreateDVD(DVD dvd)
        {
            db.DVD.Add(dvd);
            db.SaveChanges();
        }

        //deletes a dvd from the database
        public void DeleteDVD(DVD dvd)
        {
            db.DVD.Attach(dvd);
            db.DVD.Remove(dvd);
            db.SaveChanges();
        }

        //filters a list of dvds by genre
        public IList<DVD> DVDGenreFilter(IList<DVD> query, string genre)
        {
            return query.Where(d => d.DVDGenre.ToString().Equals(genre)).ToList<DVD>();
        }

        //filters a list of dvds by status
        public IList<DVD> DVDStatusFilter(IList<DVD> query, string status)
        {
            return query.Where(d => d.Status.ToString().Equals(status)).ToList<DVD>();
        }

        //filters a list of dvds by the entered searchString
        public IList<DVD> DVDTextSearch(IList<DVD> query, string searchString)
        {
            return query.Where(d => d.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                 || d.Director.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<DVD>();
        }

        //filters a list of dvds by type
        public IList<DVD> DVDTypeFilter(IList<DVD> query, string type)
        {
            return query.Where(d => d.Type.ToString().Equals(type)).ToList<DVD>();
        }

        public IList<DVD> DVDLibraryFilter(IList<DVD> query, string library)
        {
            return query.Where(d => d.LibraryId.ToString().Equals(library)).ToList<DVD>();
        }

        //saves any changes to the database
        public void UpdateDVD(DVD dvd)
        {
            db.DVD.Attach(dvd);
            db.Entry(dvd).State = EntityState.Modified;
            db.SaveChanges();
        }

        //gets a single book without tracking
        public DVD GetDVD(int id)
        {
            return db.DVD.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }

        //gets a single book with tracking
        public static DVD GetDVDWithTracking(LibraryContext context, int id)
        {
            return context.DVD.Where(b => b.id == id).FirstOrDefault();
        }

        //gets all dvds from the database(without tracking) and loads bookmarkedBy, ReservedBy, LoanedBy with it 
        public IList<DVD> GetDVDs()
        {
            return db.DVD
                .Include(b => b.BookmarkedBy)
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .AsNoTracking().ToList();
        }

        //gets all dvds that got added during the last 7 days
        public IList<DVD> GetNewDVDs()
        {
            var baselineDate = DateTime.Now.AddDays(-7);
            IList<DVD> newDVDs = db.DVD
                .Include(b => b.BookmarkedBy)
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .AsNoTracking()
                .Where(x => x.DateAdded > baselineDate)
                .OrderByDescending(x => x.DateAdded).ToList();
            return newDVDs;
        }

        //gets all reserved dvds 
        public IList<DVD> GetReservedDVDs()
        {
            IQueryable<DVD> reservedDVDs;
            reservedDVDs = from d
                           in db.DVD
                           where d.Status.Equals(Status.Reserved)
                           select d;
            return reservedDVDs.AsNoTracking().ToList<DVD>();
        }
    }
}
