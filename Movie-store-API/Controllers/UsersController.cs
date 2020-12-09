using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movie_store_API.Data;
using Movie_store_API.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie_store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        
        private readonly IMapper _mapper;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

        /// <summary>
        /// User Login Endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var userName = userDTO.Username;
            var password = userDTO.Password;

            var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                var tokenString = await GenerateJSONWebToken(user);
                return Ok(new { token = tokenString, email=user.Email, Id=user.Id, username=user.UserName });
            }

            return Unauthorized(userDTO);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO userDTO)
        {
            /*var userExists = _userManager.FindByNameAsync(userDTO.Username);
            if (userExists != null)
            {
                ModelState.AddModelError("", "Account with username already exists");
                return StatusCode(400, ModelState);
            }*/

            var user = new IdentityUser
            {
                UserName = userDTO.Username,
                Email = userDTO.Email
            };

            var result= await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"Customer");
                return Created("User was Created", new { username = user.UserName, email = user.Email });
            }
            else
            {
                ModelState.AddModelError("", "Account with Email already exists");
                return StatusCode(400, ModelState);
            }

            //var userDto = _mapper.Map<CustomerViewDTO>(user);

        }

        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                , _config["Jwt:Issuer"]
                , claims, null
                , expires: DateTime.Now.AddHours(24)
                , signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
