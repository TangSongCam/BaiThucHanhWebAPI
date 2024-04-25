﻿using BaiThucHanhWeb.Model.Domain;
namespace BaiThucHanhWeb.Model.DTO
{
    public class AdBookRequestDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int Rate { get; set; }
        public int Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        public int PublisherID { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
