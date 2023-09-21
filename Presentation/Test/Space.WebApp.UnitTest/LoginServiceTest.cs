using Space.WebApp.Models.Request;
using Space.WebApp.Models.Response;
using Space.WebApp.Service.Concretes;
using Space.Application.Models.Response;

namespace Space.WebApp.UnitTest
{
    public class LoginServiceTest
    {
        [Fact]
        public async Task Test1()
        {
            var userName = "mucahitcelebi97@gmail.com";
            var password = "123";

            var authService = new AuthService();
            var result = await authService.Login(new LoginRequest()
            {
                UserName = userName,
                Password = password
            });

            Assert.Equal(new BaseResponse<LoginResponse>(), result);
        }
    }
}