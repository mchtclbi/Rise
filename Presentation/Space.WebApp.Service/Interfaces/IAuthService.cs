using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.Application.Models.Response;

namespace Space.WebApp.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<BaseResponse<LoginResponse>> Login(LoginRequest request);
    }
}