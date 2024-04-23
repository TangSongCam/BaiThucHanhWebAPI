using System.ComponentModel.DataAnnotations;

namespace BaiThucHanhWeb.Model.Domain
{
    public class Authors
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; }

        // Thuộc tính dẫn hướng cho mối quan hệ một-nhiều với Book_Author
        public ICollection<Book_Author> BookAuthors { get; set; }
    }
}
