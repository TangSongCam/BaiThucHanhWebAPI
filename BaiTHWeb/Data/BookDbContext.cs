using BaiTHWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiTHWeb.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Book_Author> BookAuthors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Publishers> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<Books>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<Publishers>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<Book_Author>()
                .HasKey(ba => ba.ID);

            modelBuilder.Entity<Book_Author>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookID);

            modelBuilder.Entity<Book_Author>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorID);

            modelBuilder.Entity<Books>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherID);
        }
    }
}
