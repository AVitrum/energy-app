using System.Security.Claims;
using UserApi.Models;
using UserApi.Payload.Requests;
using UserApi.Payload.Responses;
using UserApi.Repositories;

namespace UserApi.Services.UserService;

public partial class UserService(
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    UserRepository userRepository)
    : IUserService
{
    public async Task RegisterAsync(RegistrationRequest request)
    {
        GeneratePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        await userRepository.CreateAsync(newUser);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        if (!await userRepository.UserExistsByUsernameAsync(request.Username))
        {
            throw new ArgumentException(nameof(User));
        }

        var user = await userRepository.GetByUsernameAsync(request.Username);

        if (!IsPasswordHashEqual(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new ArgumentException("Password is incorrect");

        return new LoginResponse
        {
            AccessToken = GenerateBearerToken(user)
        };
    }

    public async Task<User> GetByToken()
    {
        const string email = ClaimTypes.Email;
        
        return httpContextAccessor.HttpContext is not null
            ? await userRepository.GetByEmailAsync(httpContextAccessor.HttpContext.User.FindFirstValue(email)!)
            : throw new ArgumentException(email);
    }
}