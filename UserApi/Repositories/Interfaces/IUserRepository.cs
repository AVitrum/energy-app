using UserApi.Models;

namespace UserApi.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExistsByUsernameAsync(string username);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
}