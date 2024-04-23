using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaiThucHanhWeb.Model.Domain
{
    public class Book_Author
    {
        [Key]
        public int ID { get; set; }

        // Khóa ngoại
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public int AuthorID { get; set; }

        // Thuộc tính điều hướng
        public Authors Author { get; set; }
        public Books Book { get; set; }
    }
}
