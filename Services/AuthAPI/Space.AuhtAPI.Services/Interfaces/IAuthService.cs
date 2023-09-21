using Space.AuthAPI.Models.Request;
using Space.AuthAPI.Models.Response;
using Space.Application.Models.Response;

namespace Space.AuthAPI.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<BaseResponse<CreateTokenResponse>> CreateToken(CreateTokenRequest request);
    }
}