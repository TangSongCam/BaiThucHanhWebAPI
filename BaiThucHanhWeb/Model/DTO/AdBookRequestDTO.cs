using BaiThucHanhWeb.Model.Domain;
using System.ComponentModel.DataAnnotations;
namespace BaiThucHanhWeb.Model.DTO
{
    public class AdBookRequestDTO
    {
        [Required]
        [MinLength(10)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        [Range(0,5,ErrorMessage ="From 0 to 5")]
        public int Rate { get; set; }
        public int Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public int PublisherID { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
