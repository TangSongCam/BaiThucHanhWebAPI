using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using BaiThucHanhWeb.Repositories;
using System;
using BaiThucHanhWeb.Migrations;

namespace BaiThucHanhWeb
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;
        public SQLBookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BookDTO> GetAllBooks()
        {
            var allBooks = _dbContext.Books.Select(Books => new BookDTO()
            {
                ID = Books.ID,
                Title = Books.Title,
                Description = Books.Description,
                IsRead = Books.IsRead,
                DateRead = Books.IsRead ? Books.DateRead.Value : null,
                Rate = Books.Rate,
                Genre = Books.Genre.ToString(),
                CoverUrl = Books.CoverUrl,
                PublisherName = Books.Publisher.Name,
                AuthorName = Books.BookAuthors.Select(author => author.Author.FullName).ToList()
            }).ToList();
            return allBooks;
        }
        public BookDTO GetBookById(int id)
        {
            var bookWithDomain = _dbContext.Books.Where(n => n.ID == id);
            //Map Domain Model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(book => new
           BookDTO()
            {
                ID = book.ID,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre.ToString(),
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorName = book.BookAuthors.Select(author => author.Author.FullName).ToList()
            }).FirstOrDefault();
            return bookWithIdDTO;
        }
        public AdBookRequestDTO AddBook(AdBookRequestDTO addBookRequestDTO)
        {
            var bookDomainModel = new Books
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublisherID = addBookRequestDTO.PublisherID
            };
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookID = bookDomainModel.ID,
                    AuthorID = id
                };
                _dbContext.BookAuthors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return addBookRequestDTO;
        }
        public AdBookRequestDTO? UpdateBookById(int id, AdBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.ID == id);
            if (bookDomain != null)
            {
                bookDomain.Title = bookDTO.Title;
                bookDomain.Description = bookDTO.Description;
                bookDomain.IsRead = bookDTO.IsRead;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.PublisherID = bookDTO.PublisherID;
                _dbContext.SaveChanges();
            }
            var authorDomain = _dbContext.BookAuthors.Where(a => a.BookID == id).ToList();
            if (authorDomain != null)
            {
                _dbContext.BookAuthors.RemoveRange(authorDomain);
                _dbContext.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookID = id,
                    AuthorID = authorid
                };
                _dbContext.BookAuthors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return bookDTO;
        }
        public Books? DeleteBookById(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.ID == id);
            if (bookDomain != null)
            {
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }
            return bookDomain;
        }

        public List<BookDTO> GetBooksSortedByField(string field, bool ascending)
        {
            var query = _dbContext.Books.AsQueryable();

            switch (field.ToLower())
            {
                case "id":
                    query = ascending ? query.OrderBy(b => b.ID) : query.OrderByDescending(b => b.ID);
                    break;
                case "name":
                    query = ascending ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title);
                    break;
                default:
                    throw new ArgumentException("Invalid field name");
            }

            return query
                .Select(b => new BookDTO
                {
                    ID = b.ID,
                    Title = b.Title
                })
                .ToList();
        }

        public List<BookDTO> SearchBooks(string keyword)
        {
            var query = _dbContext.Books
                .Where(b => b.Title.Contains(keyword) || b.Description.Contains(keyword))
                .OrderBy(b => b.Title)
                .Select(b => new BookDTO
                {
                    ID = b.ID,
                    Title = b.Title
                })
                .ToList();

            return query;
        }

        public List<BookDTO> GetBooksPage(int pageNumber, int pageSize)
        {
            return _dbContext.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookDTO
                {
                    ID = b.ID,
                    Title = b.Title
                })
                .ToList();
        }
    }
}