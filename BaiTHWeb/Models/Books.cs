using System.ComponentModel.DataAnnotations;


namespace BaiTHWeb.Models
{
    public class Books
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int Rate { get; set; }
        public int Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // Khóa ngoại
        public int PublisherID { get; set; }

        // Thuộc tính điều hướng
        public Publishers Publisher { get; set; }
        //  Thuộc tính dẫn hướng cho mối quan hệ một-nhiều với Book_Author
        public ICollection<Book_Author> BookAuthors { get; set; }
    }
}
