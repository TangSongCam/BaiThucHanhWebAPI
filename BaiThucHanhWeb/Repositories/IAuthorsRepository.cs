using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using System.Collections.Generic;

namespace BaiThucHanhWeb.Repositories
{
    public interface IAuthorsRepository
    {
        List<AuthorsDTO> GetAllAuthors();
        AuthorsDTO GetAuthorById(int id);
        AuthorsDTO AddAuthor(AuthorsDTO author);
        AuthorsDTO UpdateAuthorById(int id, Authors author);
        Authors? DeleteAuthorById(int id);
        public List<AuthorsDTO> GetAuthorsSortedByField(string field, bool ascending);
        List<AuthorsDTO> SearchAuthors(string keyword);
        List<AuthorsDTO> GetAuthorsPage(int pageNumber, int pageSize);
    }
}
