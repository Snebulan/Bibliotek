using Bibliotek.Models;
using Bibliotek.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bibliotek.Data
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB;Initial Catalog = Library; Integrated Security = True");
        }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
                modelBuilder.Entity<Loan>().HasData(
                new Loan
                {
                    ID = 1,
                    BookID = 1,
                    Member = 1
                }
            );
            */
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    ID = 1,
                    FirstName = "Fredrik",
                    LastName = "Gustafsson",
                    PersonNumber = "19720921-2013"
                }
);
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    ID = 1,
                    FirstName = "William",
                    LastName = "Shakespeare"
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { ID = 1, AuthorID = 1, Title = "Hamlet", ISBN = "1472518381", Description = "Arguably Shakespeare's greatest tragedy" },
                new Book { ID = 2, AuthorID = 1, Title = "King Lear", ISBN = "9780141012292", Description = "King Lear is a tragedy written by William Shakespeare. It depicts the gradual descent into madness of the title character, after he disposes of his kingdom by giving bequests to two of his three daughters egged on by their continual flattery, bringing tragic consequences for all." },
                new Book { ID = 3, AuthorID = 1, Title = "Othello", ISBN = "1853260185", Description = "An intense drama of love, deception, jealousy and destruction." }
            );
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

    }
}
