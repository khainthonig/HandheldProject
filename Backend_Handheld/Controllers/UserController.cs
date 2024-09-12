using Backend_Handheld.Entities.Const;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Backend_Handheld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            List<UserDto> userDto;
            userDto = await _serviceManager.UserService.GetAll();
            if (userDto == null) userDto = new();
            return Ok(userDto);
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto createUserDto)
        {
            try
            {
                var result = await _serviceManager.UserService.Create(createUserDto);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(MessageError.ErrorCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            loginDto.Username = loginDto.Username.Trim();
            loginDto.Password = loginDto.Password.Trim();
            var getUser = await _serviceManager.UserService.GetByUsername(loginDto.Username);
            if(getUser == null) 
                return BadRequest(MessageError.UserNotExist);
            var userLogin =  await _serviceManager.UserService.Login(loginDto.Username, loginDto.Password);
            if (userLogin == null) 
                return BadRequest(MessageError.IncorrectPassword);
            return Ok(userLogin);
        }
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _serviceManager.UserService.GetById(id);
            if (dto == null) return BadRequest("No data.");
            return Ok(dto);
        }
    }
}
