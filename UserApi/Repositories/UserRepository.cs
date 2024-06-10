using Microsoft.EntityFrameworkCore;
using UserApi.Configs;
using UserApi.Models;

namespace UserApi.Repositories;

public class UserRepository(AppDbContext dbContext) : RepositoryBase<User>(dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public Task<bool> UserExistsByUsernameAsync(string username)
    {
        return _dbContext.Users.AnyAsync(u => u.Username == username);
    }
    
    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username) 
               ?? throw new ArgumentNullException(nameof(User));
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email) 
               ?? throw new ArgumentNullException(nameof(User));
    }
}