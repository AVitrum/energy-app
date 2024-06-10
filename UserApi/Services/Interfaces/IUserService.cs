using UserApi.Models;
using UserApi.Payload.Requests;
using UserApi.Payload.Responses;
using LoginRequest = UserApi.Payload.Requests.LoginRequest;

namespace UserApi.Services.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegistrationRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<User> GetByToken();
}