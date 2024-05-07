using System.Threading.Tasks;
using BaiThucHanhWeb.Model.Domain;
using BaiThucHanhWeb.Model.DTO;
using System.Collections.Generic;


namespace BaiThucHanhWeb.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
