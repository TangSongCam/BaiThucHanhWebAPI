using BaiTHWeb.Models;


namespace BaiTHWeb.Data
{
    public class AddData
    {

        private readonly BookDbContext _context;

        public AddData(BookDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Authors.Any())
            {
                _context.Authors.AddRange(
                    new Authors { FullName = "J.K. Rowling" },
                    new Authors { FullName = "Walter Isaacson" },
                    new Authors { FullName = "Isaacson" }
                );
            }

            if (!_context.Publishers.Any())
            {
                _context.Publishers.AddRange(
                    new Publishers { Name = "Publisher 1" },
                    new Publishers { Name = "Publisher 2" },
                    new Publishers { Name = "Publisher 3" }
                );
            }

            if (!_context.Books.Any())
            {
                var randomPublisher = _context.Publishers.OrderBy(p => Guid.NewGuid()).FirstOrDefault();

                var newBook = new Books
                {
                    Title = "Harry Potter và Hòn đá phù thủy",
                    Description = "Cuộc sống của Harry Potter thật khốn khổ. Cha mẹ anh đã chết và anh bị mắc kẹt với những người thân vô tâm của mình, những người buộc anh phải sống trong một tủ quần áo nhỏ dưới cầu thang.",
                    IsRead = false,
                    DateRead = null,
                    Rate = 0,
                    Genre = 0,
                    CoverUrl = "url_of_cover_image",
                    DateAdded = DateTime.Now,
                    PublisherID = randomPublisher?.ID ?? 1
                };

                _context.Books.Add(newBook);
            }

            if (!_context.BookAuthors.Any())
            {
                var randomAuthor = _context.Authors.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
                var randomBook = _context.Books.OrderBy(b => Guid.NewGuid()).FirstOrDefault();

                var newBookAuthor = new Book_Author
                {
                    BookID = randomBook?.ID ?? 1,
                    AuthorID = randomAuthor?.ID ?? 1
                };

                _context.BookAuthors.Add(newBookAuthor);
            }

            _context.SaveChanges();
        }
    }
}
