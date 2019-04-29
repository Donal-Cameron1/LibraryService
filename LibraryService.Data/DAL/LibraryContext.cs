using LibraryService.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LibraryService.DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryContext")
        {

        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DVD> DVD { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LibraryContext>(new LibraryInitialiser());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                        .HasMany(u => u.BookmarkedLibraryItems)
                        .WithMany(b => b.BookmarkedBy)
                        .Map(cs =>
                         {
                             cs.MapLeftKey("UserId");
                             cs.MapRightKey("ItemId");
                             cs.ToTable("BookmarkedLibraryItems");
                         });
        }
    }
}