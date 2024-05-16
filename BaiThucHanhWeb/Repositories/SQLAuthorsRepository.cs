using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BaiThucHanhWeb.Repositories
{
    public class SQLAuthorsRepository : IAuthorsRepository
    {
        private readonly BookDbContext _dbContext;

        public SQLAuthorsRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AuthorsDTO> GetAllAuthors()
        {
            return _dbContext.Authors.Select(author => new AuthorsDTO
            {
                Id = author.ID,
                FullName = author.FullName
            }).ToList();
        }

        public AuthorsDTO GetAuthorById(int id)
        {
            return _dbContext.Authors.Where(a => a.ID == id).Select(author => new AuthorsDTO
                {
                    Id = author.ID,
                    FullName = author.FullName
                })
                .FirstOrDefault();
        }

        public AuthorsDTO AddAuthor(AuthorsDTO authorDTO)
        {
            var author = new Authors
            {
                FullName = authorDTO.FullName
            };
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

            authorDTO.Id = author.ID;
            return authorDTO;
        }

        public AuthorsDTO UpdateAuthorById(int id, Authors author)
        {
            var existingAuthor = _dbContext.Authors.FirstOrDefault(a => a.ID == id);
            if (existingAuthor != null)
            {
                existingAuthor.FullName = author.FullName;
                _dbContext.SaveChanges();

                var updatedAuthorDTO = new AuthorsDTO
                {
                    FullName = existingAuthor.FullName
                };

                return updatedAuthorDTO;
            }
            return null;
        }



        public Authors? DeleteAuthorById(int id)
        {
            var authorToDelete = _dbContext.Authors.FirstOrDefault(a => a.ID == id);
            if (authorToDelete != null)
            {
                _dbContext.Authors.Remove(authorToDelete);
                _dbContext.SaveChanges();
            }
            return authorToDelete;
        }

        public List<AuthorsDTO> GetAuthorsSortedByField(string field, bool ascending)
        {
            var query = _dbContext.Authors.AsQueryable();

            switch (field.ToLower())
            {
                case "id":
                    query = ascending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
                case "fullname":
                    query = ascending ? query.OrderBy(a => a.FullName) : query.OrderByDescending(a => a.FullName);
                    break;
                default:
                    throw new ArgumentException("Invalid field name");
            }

            return query
                .Select(a => new AuthorsDTO
                {
                    Id = a.ID,
                    FullName = a.FullName
                })
                .ToList();
        }

        public List<AuthorsDTO> SearchAuthors(string keyword)
        {
            var query = _dbContext.Authors
                .Where(a => a.FullName.Contains(keyword))
                .OrderBy(a => a.FullName)
                .Select(a => new AuthorsDTO
                {
                    Id = a.ID,
                    FullName = a.FullName
                })
                .ToList();

            return query;
        }

        public List<AuthorsDTO> GetAuthorsPage(int pageNumber, int pageSize)
        {
            return _dbContext.Authors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AuthorsDTO
                {
                    Id = a.ID,
                    FullName = a.FullName
                })
                .ToList();
        }

    }
}
