
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

        private readonly IUserService userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<BaseApiController> logger, IUserService _userService, IMapper mapper) : base(logger)
        {
            userService = _userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserResponseDto[]> List()
        {
            try
            {
                List<User> users = userService.ListAll();
                List<UserResponseDto> userDtos = _mapper.Map<List<UserResponseDto>>(users);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDto> ListById(Guid id)
        {
            try
            {
                User user = userService.ListById(id);
                UserResponseDto userDto = _mapper.Map<UserResponseDto>(user);
                return Ok(userDto);
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPost]
        public ActionResult<UserResponseDto> Insert([FromBody] UserRequestDto user)
        {
            try
            {
                User createdUser = userService.Insert(user);
                UserResponseDto userDto = _mapper.Map<UserResponseDto>(createdUser);
                return Ok(userDto);
            }
            catch (DbUpdateException ex)
            {
                return HandleException(ex, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPut("UpdatePassword/{id}")]
        public ActionResult<string> UpdatePassword(Guid id, [FromBody] UserUpdatePasswordDto user)
        {
            try
            {
                userService.UpdatePassword(id, user);
                return Ok("Password changed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (DbUpdateException ex)
            {
                return HandleException(ex, ex.InnerException?.Message ?? ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Remove(Guid id)
        {
            try
            {
                userService.Remove(id);
                return Ok("User successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                return HandleException(ex, ex.Message, 400);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

        [HttpPost("Login")]
        public ActionResult<UserResponseDto> Login([FromBody] UserRequestDto user)
        {
            try
            {
                User updatedUser = userService.Login(user);
                UserResponseDto userDto = _mapper.Map<UserResponseDto>(updatedUser);
                return Ok(userDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return HandleException(ex, ex.Message, 401);
            }
            catch (Exception ex)
            {
                return HandleException(ex, ex.Message, 500);
            }
        }

    }
}
