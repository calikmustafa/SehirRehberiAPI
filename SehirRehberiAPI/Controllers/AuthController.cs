using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SehirRehberiAPI.Data;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private IAuthRepository _authRepository;
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterListDto userForRegisterDto)
        {
            if (await _authRepository.UserExist(userForRegisterDto.FirstName))
            {
                ModelState.AddModelError("UserName", "UserName is already exist");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                UserName = userForRegisterDto.FirstName
            };

            var createRegister = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginListDto userForLoginListDto)
        {
            var user = await _authRepository.Login(userForLoginListDto.FirstName, userForLoginListDto.Password);
            if (user==null)
            {
                return Unauthorized();
            }

            //TOKEN İŞLERİNİ KİM YAPICAK ?
            var tokenHandler = new JwtSecurityTokenHandler();

            //APP SETİNGİN İÇİN DE KEY'E İHTİYACIMIZ VAR
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            //TOKEN NELERİ TUTUCAK ?
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //TOKENDE TUTMAK İSTEDİĞİMİZ TEMEL ŞEYLER
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)

                }),
                //TOKEN NE KADAR GEÇERLİ
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString=tokenHandler.WriteToken(token);
            return Ok(tokenString);
          
        }
    }
}
