using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Model.DTO;
using Microsoft.EntityFrameworkCore;


namespace BaiThucHanhWeb.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly BookDbContext _dbContext;

        public SQLUserRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }

}
