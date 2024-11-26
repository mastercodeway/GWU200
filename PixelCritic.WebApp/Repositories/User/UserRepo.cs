
using Microsoft.EntityFrameworkCore;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;
using System.Diagnostics.Metrics;
namespace PixelCritic.WebApp.Repositories
{
    public class UserRepo : BaseRepo<User, Guid>, IUserRepo
    {
        private readonly PixelDbContext _context;
        public UserRepo(PixelDbContext context) :base(context)
        {
            _context = context;
        }
       
        public async Task<bool> AddUserAsync(User user)
        {
            if(user is null) return false;

            try
            {
                var foundUser = await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Username == user.Username);

                if(foundUser is null)
                {
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return false;
            }

            return false;
        }
        public async Task<UserLoginDto?> GetUSerAsync(string Username)
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    throw new ArgumentNullException(nameof(Username));
                }

                return UserLoginDto.MapToDto( await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Username == Username));
            
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return null;
            }
           
        }

        
 
    }
}
