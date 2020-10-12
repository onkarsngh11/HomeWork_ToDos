using System.Threading.Tasks;
using AutoMapper;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Helpers;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HomeWork_ToDos.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Route("api/v{version:apiVersion}/todo/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserContract _userService;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public UserController(ILogger<UserController> logger, IUserContract userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Takes UserName and Password and generates token on successful authentication. 
        /// </summary>
        /// <param name="loginModel">Conatains UserName,Password </param>
        /// <returns>ApiResponse on User Login </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            _logger.LogInformation("Started : Logging In.");
            UserDto userDto = await _userService.AuthenticateUser(loginModel.UserName, loginModel.Password);

            if (userDto != null)
            {
                // On Successful authentication, generate jwt token.
                string token = JwtTokenHelper.GenerateJwtToken(userDto, _appSettings);

                return Ok(
                    new ApiResponse<string>
                    {
                        IsSuccess = true,
                        Result = token,
                        Message = "Authentication successful."
                    });
            }
            else
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "Authentication failed.",
                        Message = "Username or password is incorrect."
                    });
            }
        }
        /// <summary>
        /// Register users.
        /// </summary>
        /// <param name="createUserModel"></param>
        /// <returns>Api Response based on success/failure.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserModel createUserModel)
        {
            _logger.LogInformation("Started : Registering User.");
            CreateUserDto userDto = _mapper.Map<CreateUserDto>(createUserModel);
            bool _registrationSuccess = await _userService.RegisterUser(userDto);
            if (_registrationSuccess)
            {
                return Ok(
                    new ApiResponse<string>
                    {
                        IsSuccess = true,
                        Result = "Success.",
                        Message = "Registration successful."
                    });
            }
            else
            {
                return BadRequest(
                    new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Result = "Fail.",
                        Message = "Some error occurred. Please refresh page and try again."
                    });
            }
        }
    }
}
