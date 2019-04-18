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
        private LibraryContext db = DbUtils.db;

        public void CreateDVD(DVD dvd)
        {
            db.DVD.Add(dvd);
            db.SaveChanges();
        }

        public void DeleteDVD(DVD dvd)
        {
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
            return query.Where(d => d.Title.Contains(searchString)
                                 || d.Director.Contains(searchString)).ToList<DVD>();
        }

        public IList<DVD> DVDTypeFilter(IList<DVD> query, string type)
        {
            return query.Where(d => d.Type.ToString().Equals(type)).ToList<DVD>();
        }

        public void EditDVD(DVD dvd)
        {
            db.Entry(dvd).State = EntityState.Modified;
            db.SaveChanges();
        }

        public DVD GetDVD(int id)
        {
            return db.DVD.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }

        public IList<DVD> GetDVDs()
        {
            IQueryable<DVD> dvdquery;
            dvdquery = from d in db.DVD select d;
            return dvdquery.ToList<DVD>();

        }

        public IList<DVD> GetNewDVDs()
        {
            var baselineDate = DateTime.Now.AddDays(-7);
            IList<DVD> newDVDs = db.DVD.Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList();
            return newDVDs;
        }
    }
}
