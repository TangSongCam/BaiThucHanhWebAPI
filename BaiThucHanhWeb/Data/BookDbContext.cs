using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BaiThucHanhWeb.Model.Domain;

namespace BaiThucHanhWeb.Data
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Authors>()
                .HasKey(a => a.ID);

            builder.Entity<Books>()
                .HasKey(b => b.ID);

            builder.Entity<Publishers>()
                .HasKey(p => p.ID);

            builder.Entity<Book_Author>()
                .HasKey(ba => ba.ID);

            builder.Entity<Book_Author>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookID);

            builder.Entity<Book_Author>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorID);

            builder.Entity<Books>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherID);

            new AddData(builder).SeedData();
        }
    }
}
