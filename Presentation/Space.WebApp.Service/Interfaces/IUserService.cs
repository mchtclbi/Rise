using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.Application.Models.Response;

namespace Space.WebApp.Service.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<UserRegisterResponse>> Register(UserRegisterRequest request);
    }
}