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
        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromForm]UserRegisterViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Password = encMD5(userModel.Password);
                var user = _mapper.Map<UserRegisterViewModel, User>(userModel);
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
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers(int? skip, int? take)
        {
            var user = await _userService.GetAllUsersAsync();
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserShowTableViewModel>>(user.Skip((int)skip));
            return  Ok(ResponseSuccess.Create(result));
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
    }
}
