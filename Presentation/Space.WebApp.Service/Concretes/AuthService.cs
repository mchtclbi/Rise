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
    public class AuthService : IAuthService
    {
        private readonly AuthApiUrl _authApiUrl;
        private readonly SendRestRequest _sendRestRequest;

        public AuthService()
        {
            _authApiUrl ??= ReadConfig.Get<AuthApiUrl>("AuthApi");
            _sendRestRequest ??= new SendRestRequest(_authApiUrl.BaseUrl);
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            var response = new BaseResponse<LoginResponse>();

            var loginValidation = LoginValidation(request.UserName, request.Password);
            if (!loginValidation)
            {
                response.SetMessage("validation failed");
                return response;
            }

            var serviceResponse = await _sendRestRequest.RunAsync<BaseResponse<LoginResponse>>(new RestRequestModel()
            {
                Endpoint = _authApiUrl.Endpoint.Login,
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

        private bool LoginValidation(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)) return false;
            if (string.IsNullOrEmpty(password)) return false;

            return true;
        }
    }
}