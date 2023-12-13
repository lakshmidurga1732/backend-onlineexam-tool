using AutoMapper;
using OnlineExam1.Models;
using OnlineExam1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineExam1.DTO;
using OnlineExam1.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using log4net;
using System.Net.Http.Headers;

namespace OnlineExam1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> _logger;
        private readonly IEmailService _emailService;



        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILogger<UserController> logger, IEmailService emailService)

        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
            _emailService = emailService;
        }
        [HttpGet, Route("GetAllUsers")]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                List<UserDTO> usersDto = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, users);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);


                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Register")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.CreateUser(user);
                return StatusCode(200, user);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);

            }
        }
        //PUT /EditUser
        [HttpPut, Route("EditUser")]
        [AllowAnonymous]
        public IActionResult EditUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.EditUser(user);
                return StatusCode(200, user);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpGet, Route("GetUserById/{userId}")]
        [AllowAnonymous]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                User user = userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found");
                }

                UserDTO userDto = _mapper.Map<UserDTO>(user);
                return StatusCode(200, userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthReponse authReponse = new AuthReponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.UserName = user.Name;
                    authReponse.Role = user.Role;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email,user.Email),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        //[HttpPost, Route("SendEmail")]
        //public IActionResult SendEmail([FromBody] EmailDTO emailDTO)
        //{
        //    try
        //    {
        //        _emailService.SendEmail(emailDTO.To, emailDTO.Subject, emailDTO.Body);
        //        return StatusCode(200, "Email sent successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogError(ex.Message);
        //        //return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost, Route("UploadImage")]
        [AllowAnonymous]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}