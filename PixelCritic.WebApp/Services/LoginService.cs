
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories;

namespace PixelCritic.WebApp.Services
{
   // constructor
    public class LoginService(
        IUnitOfWork unitOfWork
        ) : ILoginService
    {
        
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
    // hashar password med bcryt algorimt
        public string HashPassword(string password)
        {
            
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        // register användare genom att lägga till det i databasen-

        public async Task<bool> Register(UserRegisterDto userRegisterDto)
        {
            var password = userRegisterDto.Password;
            var hashedPassword = HashPassword(password);
            
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Password = hashedPassword,
            };

            var userAddedSuccessfully = await _unitOfWork.UserRepo.AddUserAsync(user);
            await _unitOfWork.SaveAsync();

            return userAddedSuccessfully;
        }
        // https://learn.microsoft.com/en-us/answers/questions/939712/how-do-i-check-the-password-entered-by-the-user-bc
        // kollar användar input innan verfierar om lösen ordet stämmer mot databasen som användaren angett genom att hitta användaren i databasen. 
        public async Task<bool> Authenticate(UserLoginDto dto)
        {
            if (dto is null) return false;
            if (string.IsNullOrEmpty(dto.Password)) return false;
            if (string.IsNullOrEmpty(dto.Username)) return false;
            
            var user = await _unitOfWork.UserRepo.GetUSerAsync(dto.Username);
            if (user is null) return false;
            
            var verified =  BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (verified is false) return false;

            dto.Id = user.Id;

            return verified;
        }
    }
}
