using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using System.Collections.Generic;

namespace BaiThucHanhWeb.Repositories
{
    public interface IPublishersRepository
    {
        List<PublishersDTO> GetAllPublishers();
        PublishersDTO GetPublisherById(int id);
        PublishersDTO AddPublisher(PublishersDTO publisher);
        PublishersDTO UpdatePublisherById(int id, PublishersDTO publisher);
        Publishers? DeletePublisherById(int id);
        public List<PublishersDTO> GetPublishersSortedByField(string field, bool ascending);
    }
}
