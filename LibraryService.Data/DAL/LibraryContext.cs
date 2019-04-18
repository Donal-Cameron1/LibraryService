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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                        .HasMany(u => u.BookmarkedBooks)
                        .WithMany(b => b.BookmarkedBy)
                        .Map(cs =>
                         {
                             cs.MapLeftKey("UserId");
                             cs.MapRightKey("BookId");
                             cs.ToTable("UserBookmarkedBooks");
                         });

           /* modelBuilder.Entity<User>()
                       .HasMany(u => u.LoanedBooks)
                       .WithOptional(b => b.LoanedBy)
                       .HasForeignKey(u => u.LoanedById);
                       

            
            modelBuilder.Entity<User>()
                       .HasMany(u => u.ReservedBooks)
                       .WithRequired(b => b.ReservedBy)
                       .HasForeignKey(u => u.id);

            */

        }

        public DbSet<LibraryItem> LibraryItems { get; set; }
    }
}