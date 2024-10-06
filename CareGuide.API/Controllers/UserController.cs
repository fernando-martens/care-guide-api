
using AutoMapper;
using CareGuide.Core.Interfaces;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.API.Controllers
{

    public class UserController : BaseApiController
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<BaseApiController> logger, IUserService userService, IMapper mapper) : base(logger)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserResponseDto[]> List()
        {
            List<User> users = _userService.ListAll();
            List<UserResponseDto> userDtos = _mapper.Map<List<UserResponseDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDto> ListById(Guid id)
        {
            User user = _userService.ListById(id);
            UserResponseDto userDto = _mapper.Map<UserResponseDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult<UserResponseDto> Insert([FromBody] UserRequestDto user)
        {
            User createdUser = _userService.Insert(user);
            UserResponseDto userDto = _mapper.Map<UserResponseDto>(createdUser);
            return Ok(userDto);
        }

        [HttpPut("UpdatePassword/{id}")]
        public ActionResult<string> UpdatePassword(Guid id, [FromBody] UserUpdatePasswordDto user)
        {
            _userService.UpdatePassword(id, user);
            return Ok("Password changed successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Remove(Guid id)
        {
            _userService.Remove(id);
            return Ok("User successfully deleted.");
        }

        [HttpPost("Login")]
        public ActionResult<UserResponseDto> Login([FromBody] UserRequestDto user)
        {
            User updatedUser = _userService.Login(user);
            UserResponseDto userDto = _mapper.Map<UserResponseDto>(updatedUser);
            return Ok(userDto);
        }

    }
}
