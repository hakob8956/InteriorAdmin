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
        private IQueryable<User> OrderTable(IQueryable<User> data, string columnName, bool desc)
        {
            switch (columnName)
            {
                case "userName":
                    if (desc)
                        return data.OrderBy(x => x.Username);
                    else
                        return data.OrderByDescending(x => x.Username);
                case "Id":
                    if (desc)
                        return data.OrderBy(x => x.Id);
                    else
                        return data.OrderByDescending(x => x.Id);
                default:
                    return null;
            }
        }
        public async Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip, int? take, bool? desc, string columnName)
        {
            try
            {
                var lenght = await _context.Users.CountAsync();
                IQueryable<User> data = null;
                if (skip != null || take != null)
                    data = _context.Users.Skip((int)skip).Take((int)take);
                else
                    data = _context.Users;
                if (desc != null && columnName != null)
                    data = OrderTable(data, columnName, (bool)desc);

                return (await data.AsNoTracking().ToListAsync(), lenght);
            }
            catch (Exception e)
            {
                return (null, 0);
            }
        }
        public async Task<(IEnumerable<User>, int count)> GetAllUsersAsync(int? skip, int? take, bool? desc, string columnName, string roleName)
        {
            try
            {
                var lenght = await _context.Users.CountAsync();
                IQueryable<User> data = null;
                if (skip != null || take != null)
                    data = _context.Users.Where(r => r.Role.Name == roleName).Take((int)take).Skip((int)skip);
                else
                    data = _context.Users.Where(r => r.Role.Name == roleName);

                if (desc != null && columnName != null)
                    data = OrderTable(data, columnName, (bool)desc);

                return (await data.AsNoTracking().ToListAsync(), lenght);
            }
            catch (Exception)
            {
                return (null, 0);
            }
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Include(r => r.Role).SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ResultCode> UpdateUserAsync(User user)
        {
            try
            {
                var dbUser = await _context.Users.AsNoTracking()
                       .SingleOrDefaultAsync(x => x.Id == user.Id);
                if (dbUser == null)
                    return ResultCode.Error;

                user.Token = dbUser.Token;
                user.Password = dbUser.Password;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return ResultCode.Success;
            }
            catch (Exception e)
            {
                return ResultCode.Error;
            }
        }

        public async Task<ResultCode> ChangeUserPasswordAsync(int id, string password)
        {
            try
            {
                var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (currentUser != null)
                {

                    currentUser.Password = password;
                    _context.Update(currentUser);
                    await _context.SaveChangesAsync();
                }
                return ResultCode.Success;
            }
            catch (Exception)
            {
                return ResultCode.Error;
            }
        }
    }
}
