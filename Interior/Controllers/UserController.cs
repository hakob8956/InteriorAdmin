using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interior.Models.Entities;
using Interior.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interior.Enums;
using System.Security.Cryptography;
using System.Text;
using Interior.Models.ViewModels;
using AutoMapper;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromForm]UserRegisterByUserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Password = encMD5(userModel.Password);
                var user = _mapper.Map<UserRegisterByUserViewModel, User>(userModel);
                var result = await _userService.CreateUserAsync(user);
                if (result == ResultCode.Success)
                {
                    if (userModel.IsRemember)
                    {

                        var resultUser = await _userService.AuthenticateAsync(userModel.Username, userModel.Password);
                        if (resultUser == null)
                            return BadRequest(ResponseError.Create("Authentication faild"));
                        else
                            return Ok(ResponseSuccess.Create(resultUser));
                    }
                    return Ok(ResponseSuccess.Create("Ok"));
                }
            }

            return BadRequest(ResponseError.Create("InValid data"));
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm]UserLoginViewModel userLogin)
        {
            userLogin.Password = encMD5(userLogin.Password);
            var user = await _userService.AuthenticateAsync(userLogin.Username, userLogin.Password);

            if (user == null)
                return BadRequest(ResponseError.Create("Username or password is incorrect"));

            return Ok(ResponseSuccess.Create(user));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers(int? skip, int? take, string dir, string field, string roleName)
        {
            bool? desc = null;
            if (!string.IsNullOrEmpty(dir) && dir != "undefined")
                desc = dir == "asc" ? false : true;

            (IEnumerable<User>, int count) tuple = (null, 0);
            if (roleName == null)
                tuple = await _userService.GetAllUsersAsync(skip, take, desc, field);
            else
                tuple = await _userService.GetAllUsersAsync(skip, take, desc, field, roleName);

            var newData = _mapper.Map<IEnumerable<User>, IEnumerable<UserShowTableViewModel>>(tuple.Item1);
            var result = new
            {
                data = newData,
                lenght = tuple.count
            };
            return Ok(ResponseSuccess.Create(result));
        }
        private string encMD5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = password.ToLower();
            return result;
        }

        [HttpGet("get-byId/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user != null)
            {
                var result = _mapper.Map<User, UserRegisterByAdminViewModel>(user);

                result.Password = null;
                result.RoleName = user.Role.Name;
                return Ok(ResponseSuccess.Create(result));
            }
            return Ok(ResponseError.Create("User not found"));
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUserByAdmin([FromBody]UserRegisterByAdminViewModel userRegister)
        {
            var result = ResultCode.Error;
            var userModel = _mapper.Map<UserRegisterByAdminViewModel, User>(userRegister);
            result = await _userService.CreateUserAsync(userModel);

            if (result == ResultCode.Error)
                return Ok(ResponseSuccess.Create("Ok"));
            else
                return Ok(ResponseError.Create("Error"));
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUserByAdmin([FromBody]UserRegisterByAdminViewModel userRegister)
        {
            var result = ResultCode.Error;
            var userModel = _mapper.Map<UserRegisterByAdminViewModel, User>(userRegister);
            var user = await _userService.GetByIdAsync(userModel.Id);
            if (userModel.Password == null)
                userModel.Password = user.Password;
            result = await _userService.UpdateUserAsync(userModel);

            if (result == ResultCode.Error)
                return Ok(ResponseSuccess.Create("Ok"));
            else
                return Ok(ResponseError.Create("Error"));
        }
    }
}
