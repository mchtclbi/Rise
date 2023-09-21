using Space.UserAPI.Models;
using Space.Application.Helper;
using Space.UserAPI.Models.Request;
using Space.UserAPI.Models.Entities;
using Space.UserAPI.Models.Response;
using Space.UserAPI.Data.Interfaces;
using Space.UserAPI.Service.Interfaces;
using Space.Application.Models.Response;

namespace Space.UserAPI.Service.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task<BaseResponse<AddUserResponse>> Add(AddUserRequest request)
        {
            var response = new BaseResponse<AddUserResponse>();

            try
            {
                await _repository.Add(new User()
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    EMail = request.EMail,
                    Password = request.Password,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                });

                response.SetMessage(ConstantMessage.AddUser, true);
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }

        public async Task<BaseResponse<UserConfirmResponse>> UserConfirm(UserConfirmRequest request)
        {
            var response = new BaseResponse<UserConfirmResponse>();

            try
            {
                var passwordHash = Encrypt.MD5(request.Password);

                var user = await _repository.Get(q => q.EMail.Equals(request.UserName) && q.Password.Equals(passwordHash) && q.IsActive);
                if (user is null)
                {
                    response.SetMessage(ConstantMessage.UserNofFound);
                    return response;
                }

                response.SetMessage(ConstantMessage.UserConfirm, true);
                response.Data = new UserConfirmResponse()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    EMail = user.EMail
                };
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }
    }
}