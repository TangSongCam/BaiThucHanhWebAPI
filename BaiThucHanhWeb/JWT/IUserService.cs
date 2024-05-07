using BaiThucHanhWeb.Data;
using BaiThucHanhWeb.Repositories;
using System.Threading.Tasks;
using BaiThucHanhWeb.Model.DTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


public class UserRepository : IUserRepository
{
    private readonly BookDbContext _dbContext;

    public UserRepository(BookDbContext dbContext)
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
