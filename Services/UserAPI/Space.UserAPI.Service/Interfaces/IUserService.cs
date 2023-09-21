using Space.UserAPI.Models.Request;
using Space.UserAPI.Models.Response;
using Space.Application.Models.Response;

namespace Space.UserAPI.Service.Interfaces
{
    public interface IUserService
    {
        public Task<BaseResponse<AddUserResponse>> Add(AddUserRequest request);
        public Task<BaseResponse<UserConfirmResponse>> UserConfirm(UserConfirmRequest request);
    }
}