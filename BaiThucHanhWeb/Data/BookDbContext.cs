using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;

namespace BaiThucHanhWeb.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Book_Author> BookAuthors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Image> images { get; set; }

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

            builder.Entity<Image>()
                .HasKey(i => i.Id);

            // Nếu có SeedData trong AddData thì giữ lại
            new AddData(builder).SeedData();
        }
    }
}
