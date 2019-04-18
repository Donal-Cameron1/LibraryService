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

        public void EditDVD(DVD dvd)
        {
            db.Entry(dvd).State = EntityState.Modified;
            db.SaveChanges();
        }

        public DVD GetDVD(int id)
        {
            return db.DVD.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }
    }
}
