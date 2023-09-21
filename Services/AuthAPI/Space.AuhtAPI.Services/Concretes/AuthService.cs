﻿using RestSharp;
using System.Text;
using Space.RestHelper;
using Space.AuthAPI.Models;
using System.Security.Claims;
using Space.RestHelper.Models;
using Space.RestHelper.Concretes;
using Space.AuthAPI.Models.Request;
using Space.AuthAPI.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Space.AuthAPI.Service.Interfaces;
using Space.Application.Models.Response;
using Microsoft.Extensions.Configuration;

namespace Space.AuthAPI.Service.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly JWTModel _jwtModel;
        private readonly UserApiUrl _userApiUrl;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtModel ??= _configuration.GetSection("JWt").Get<JWTModel>();
            _userApiUrl ??= _configuration.GetSection("Url").GetSection("UserApi").Get<UserApiUrl>();
        }

        public async Task<BaseResponse<CreateTokenResponse>> CreateToken(CreateTokenRequest request)
        {
            var response = new BaseResponse<CreateTokenResponse>();

            try
            {
                SendRestRequest restRequest = new SendRestRequest(_userApiUrl.BaseUrl);
                var serviceResponse = await restRequest.RunAsync<BaseResponse<UserConfirmResponse>>(new RestRequestModel()
                {
                    Endpoint = _userApiUrl.Endpoint.UserConfirm,
                    Method = Method.Post,
                    Content = new JsonContent(),
                    Data = request
                });

                if (serviceResponse == null || serviceResponse.Data == null || string.IsNullOrEmpty(serviceResponse.Content))
                {
                    response.SetMessage("user informations not read.");
                    return response;
                }

                response.SetMessage("transaction is success", true);
                response.Data = new CreateTokenResponse()
                {
                    UserId = serviceResponse.Data.Data.Id,
                    Token = CreateJWTToken(serviceResponse.Data.Data.Id)
                };
            }
            catch (Exception)
            {
                response.SetMessage("Please try again later!");
            }

            return response;
        }

        private string CreateJWTToken(Guid id)
        {
            var key = Encoding.ASCII.GetBytes(_jwtModel.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtModel.Issuer,
                Audience = _jwtModel.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtModel.ExpireTime),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}