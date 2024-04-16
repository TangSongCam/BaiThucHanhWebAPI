using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaiTHWeb.Models
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
