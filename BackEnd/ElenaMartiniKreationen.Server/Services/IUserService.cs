using ElenaMartiniKreationen.Server.Request;
using ElenaMartiniKreationen.Server.Response;

namespace ElenaMartiniKreationen.Server.Services
{
    public interface IUserService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
        Task<BaseResponse> RegisterAsync(RegisterUserDto request);
    }
}
