
using PixelCritic.WebApp.Dtos;


namespace PixelCritic.WebApp.Services
{
    public interface ILoginService
    {
        public Task<bool> Register(UserRegisterDto userRegisterDto);
        public Task<bool> Authenticate(UserLoginDto dto);
        public string HashPassword(string password);
    }

}
