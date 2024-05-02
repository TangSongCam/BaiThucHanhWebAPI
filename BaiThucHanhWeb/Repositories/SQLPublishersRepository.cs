using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BaiThucHanhWeb.Repositories
{
    public class SQLPublishersRepository : IPublishersRepository
    {
        private readonly BookDbContext _dbContext;

        public SQLPublishersRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublishersDTO> GetAllPublishers()
        {
            return _dbContext.Publishers
                .Select(p => new PublishersDTO
                {
                    ID = p.ID,
                    Name = p.Name
                })
                .ToList();
        }

        public PublishersDTO GetPublisherById(int id)
        {
            return _dbContext.Publishers
                .Where(p => p.ID == id)
                .Select(p => new PublishersDTO
                {
                    ID = p.ID,
                    Name = p.Name
                })
                .FirstOrDefault();
        }

        public PublishersDTO AddPublisher(PublishersDTO publisherDTO)
        {
            var publisher = new Publishers { Name = publisherDTO.Name };
            _dbContext.Publishers.Add(publisher);
            _dbContext.SaveChanges();
            publisherDTO.ID = publisher.ID;
            return publisherDTO;
        }

        public PublishersDTO UpdatePublisherById(int id, PublishersDTO publisherDTO)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.ID == id);
            if (publisher != null)
            {
                publisher.Name = publisherDTO.Name;
                _dbContext.SaveChanges();
            }
            return publisherDTO;
        }



        public Publishers? DeletePublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.ID == id);
            if (publisher != null)
            {
                _dbContext.Publishers.Remove(publisher);
                _dbContext.SaveChanges();
            }
            return publisher;
        }

        public List<PublishersDTO> GetPublishersSortedByField(string field, bool ascending)
        {
            var query = _dbContext.Publishers.AsQueryable();

            switch (field.ToLower())
            {
                case "id":
                    query = ascending ? query.OrderBy(p => p.ID) : query.OrderByDescending(p => p.ID);
                    break;
                case "name":
                    query = ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                    break;
                default:
                    throw new ArgumentException("Invalid field name");
            }

            return query
                .Select(p => new PublishersDTO
                {
                    ID = p.ID,
                    Name = p.Name
                })
                .ToList();
        }
    }
}
