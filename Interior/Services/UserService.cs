using Interior.Enums;
using Interior.Helpers;
using Interior.Models.EFContext;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Interior.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public async Task<ResultCode> CreateUserAsync(User user)
        {
            try
            {
                var dbUser = await _context.Users
                       .SingleOrDefaultAsync(x => x.Id == user.Id);
                if (dbUser != null)
                    return ResultCode.Error;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return ResultCode.Success;
            }
            catch (Exception e)
            {
                return ResultCode.Error;
            }
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;
            var roleName = (await _context.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId)).Name;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roleName)
                }),
                // Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            await _context.SaveChangesAsync();
            user.Password = null;
            return user;
        }

        public async Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip, int? take)
        {
            try
            {

                var lenght = await _context.Users.CountAsync();
                List<User> data = null;
                if (skip != null || take != null)
                    data = await _context.Users.Skip((int)skip).Take((int)take).AsNoTracking().ToListAsync();
                else
                    data = await _context.Users.AsNoTracking().ToListAsync();

                return (data, lenght);
            }
            catch (Exception)
            {
                return (null, 0);
            }
        }
        public async Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip, int? take,string roleName)
        {
            try
            {
                var lenght = await _context.Users.CountAsync();
                List<User> data = null;
                if (skip != null || take != null)
                    data = await _context.Users.Where(r=>r.Role.Name==roleName).Take((int)take).Skip((int)skip).AsNoTracking().ToListAsync();
                else
                    data = await _context.Users.Where(r=>r.Role.Name==roleName).AsNoTracking().ToListAsync();
                return (data, lenght);
            }
            catch (Exception)
            {
                return (null, 0);
            }
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ResultCode> UpdateUserAsync(User user)
        {
            try
            {
                var dbUser = await _context.Users
                       .SingleOrDefaultAsync(x => x.Id == user.Id);
                if (dbUser == null)
                    return ResultCode.Error;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
    }
}
