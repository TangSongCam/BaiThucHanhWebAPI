using System.ComponentModel.DataAnnotations;

namespace BaiThucHanhWeb.Model.Domain
{
    public class Publishers
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        // Thuộc tính điều hướng cho mối quan hệ một-nhiều với Sách
        public ICollection<Books> Books { get; set; }
    }
}
