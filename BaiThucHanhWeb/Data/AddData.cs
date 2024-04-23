using BaiThucHanhWeb.Model;
using BaiThucHanhWeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;


namespace BaiThucHanhWeb.Data
{
    public class AddData
    {
        private readonly ModelBuilder _builder;

        public AddData(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void SeedData()
        {
            _builder.Entity<Authors>().HasData(
                new Authors { ID = 1, FullName = "Paulo Coelho" },
                new Authors { ID = 2, FullName = "J.K. Rowling" },
                new Authors { ID = 3, FullName = "Jeff Kinney" },
                new Authors { ID = 4, FullName = "Harper Lee" },
                new Authors { ID = 5, FullName = "J.D. Salinger" }
            );

            _builder.Entity<Publishers>().HasData(
                new Publishers { ID = 101, Name = "HarperCollins" },
                new Publishers { ID = 102, Name = "Bloomsbury (UK), Scholastic (US)" },
                new Publishers { ID = 103, Name = "Amulet Books (US), Puffin Books (UK)" },
                new Publishers { ID = 104, Name = "J.B. Lippincott & Co." },
                new Publishers { ID = 105, Name = "Little, Brown and Company" }
            );

            _builder.Entity<Books>().HasData(
                new Books
                {
                    ID = 201,
                    Title = "The Alchemist",
                    Description = "The Alchemist is about a shepherd named Santiago who embarks on a journey to fulfill his dreams and find the true meaning of life.",
                    IsRead = false,
                    DateRead = new DateTime(2024, 4, 16),
                    Rate = 5,
                    Genre = 1,
                    CoverUrl = "https://www.tailieuielts.com/wp-content/uploads/2022/01/The-Alchemist-676x1024.jpg",
                    DateAdded = new DateTime(2024, 4, 16),
                    PublisherID = 101 
                },
                new Books
                {
                    ID = 202,
                    Title = "Harry Potter",
                    Description = "Harry Potter series follows the adventures of a young wizard at Hogwarts School of Witchcraft and Wizardry.",
                    IsRead = false,
                    DateRead = new DateTime(2024, 4, 15),
                    Rate = 4,
                    Genre = 2,
                    CoverUrl = "https://www.tailieuielts.com/wp-content/uploads/2022/01/Harry-Potter.jpg",
                    DateAdded = new DateTime(2024, 4, 15),
                    PublisherID = 102
                },
                new Books
                {
                    ID = 203,
                    Title = "Diary of a Wimpy Kid",
                    Description = "Diary of a Wimpy Kid series humorously documents the ups and downs of middle school life through the diary entries of protagonist Greg Heffley.",
                    IsRead = false,
                    DateRead = new DateTime(2024, 4, 14),
                    Rate = 3,
                    Genre = 3,
                    CoverUrl = "https://www.tailieuielts.com/wp-content/uploads/2022/01/diary-of-a-wimpy-kid.jpg",
                    DateAdded = new DateTime(2024, 4, 14),
                    PublisherID = 103 
                },
                new Books
                {
                    ID = 204,
                    Title = "To Kill a Mockingbird",
                    Description = "To Kill a Mockingbird is a classic novel that explores themes of racial injustice and moral growth through the eyes of young Scout Finch in the American South of the 1930s.",
                    IsRead = false,
                    DateRead = new DateTime(2024, 4, 13),
                    Rate = 2,
                    Genre = 4,
                    CoverUrl = "https://www.tailieuielts.com/wp-content/uploads/2022/01/to-kill-a-mockingbird.jpg",
                    DateAdded = new DateTime(2024, 4, 13),
                    PublisherID = 104 
                },
                new Books
                {
                    ID = 205,
                    Title = "The Catcher in the Rye",
                    Description = "The Catcher in the Rye is a classic novel that explores teenage angst and rebellion against societal phoniness.",
                    IsRead = false,
                    DateRead = new DateTime(2024, 4, 12),
                    Rate = 1,
                    Genre = 5,
                    CoverUrl = "https://www.tailieuielts.com/wp-content/uploads/2022/01/the-catcher-in-the-rye.jpg",
                    DateAdded = new DateTime(2024, 4, 12),
                    PublisherID = 105 // Sử dụng PublishersID thay vì Idpublisher
                }
            );
        }
    }
}