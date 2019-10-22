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

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(ResponseError.Create("Username or password is incorrect"));
 
            return Ok(ResponseSuccess.Create(user));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        //private static string EncMD5(string password)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    UTF8Encoding encoder = new UTF8Encoding();
        //    Byte[] originalBytes = encoder.GetBytes(password);
        //    Byte[] encodedBytes = md5.ComputeHash(originalBytes);
        //    password = BitConverter.ToString(encodedBytes).Replace("-", "");
        //    var result = password.ToLower();
        //    return result;
        //}
    }
}
