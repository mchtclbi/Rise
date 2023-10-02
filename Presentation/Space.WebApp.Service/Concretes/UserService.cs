using RestSharp;
using Space.RestHelper;
using Space.WebApp.Models;
using Space.RestHelper.Models;
using Space.Application.Helper;
using Space.RestHelper.Concretes;
using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.WebApp.Service.Interfaces;
using Space.Application.Models.Response;

namespace Space.WebApp.Service.Concretes
{
    public class UserService : IUserService
    {
        private readonly UserApiUrl _userApiUrl;
        private readonly SendRestRequest _sendRestRequest;

        public UserService()
        {
            _userApiUrl ??= ReadConfig.Get<UserApiUrl>("UserApi");
            _sendRestRequest ??= new SendRestRequest(_userApiUrl.BaseUrl);
        }

        public async Task<BaseResponse<UserRegisterResponse>> Register(UserRegisterRequest request)
        {
            var response = new BaseResponse<UserRegisterResponse>();

            var registerValidation = RegisterValidation(request);
            if (!registerValidation)
            {
                response.SetMessage("validation failed");
                return response;
            }

            var serviceResponse = await _sendRestRequest.RunAsync<BaseResponse<UserRegisterResponse>>(new RestRequestModel()
            {
                Endpoint = _userApiUrl.Endpoint.Register,
                Data = request,
                Method = Method.Post,
                Content = new JsonContent()
            });

            if (!serviceResponse.Data.IsSuccess)
            {
                response.SetMessage("Please try again later!");
                return response;
            }

            response = serviceResponse.Data;


            return response;
        }

        private bool RegisterValidation(UserRegisterRequest request)
        {
            //To Do: validation with fluent validation

            return true;
        }
    }
}