using TaskManagnmentApi.Dtos;

namespace TaskManagnmentApi.Services;

public interface IAuthService
{
    Task<string> LoginAsync(UserLoginDto loginDto);
    Task RegisterAsync(UserLoginDto registerDto);
}