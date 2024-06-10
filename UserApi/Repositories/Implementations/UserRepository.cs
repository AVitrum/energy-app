using Microsoft.EntityFrameworkCore;
using UserApi.Configs;
using UserApi.Models;
using UserApi.Repositories.Interfaces;

namespace UserApi.Repositories.Implementations;

public class UserRepository(AppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<bool> UserExistsByUsernameAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
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