using Application.Constant;
using Application.Interface;
using Application.RequestModel;
using Application.ResponseModels;
using AutoMapper;
using Domain.Models.Identity;
using Infrastructure.Constant;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IPermissionService _permissionService;
        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager,
            IConfiguration configuration, IMapper mapper, IAuthService authService, IUserService userService, ILogger<AuthController> logger,
            IPermissionService permissionService)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _authService = authService;
            _userService = userService;
            _logger = logger;
            _permissionService = permissionService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<LoginResponseModel> Login(LoginRequestModel model)
        {
            _logger.LogInformation("User {Email} is login", model.Email);
            var user = _userManager.Users.SingleOrDefault(x => x.Email == model.Email.Trim() && !x.IsDeleted);
            if (user == null)
            {
                throw new UnAuthorizedException(MessageConstants.UsernamePasswordDoNotMatch);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return new LoginResponseModel()
                {
                    Token = await _authService.GenerateToken(user),
                    StatusCode = AppStatusCode.SuccessStatusCode.ToString(),
                    Message = MessageConstants.LoginSuccess,
                    Permissions = await _permissionService.GetPermissionsByUserId(user.Id),
                    UserProfile = await _userService.GetUserById(user.Id)
                };
            }

            throw new UnAuthorizedException(MessageConstants.UsernamePasswordDoNotMatch);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterRequestModel model)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == model.Email.ToLower()))
            {
                return BadRequest("Username is taken");
            }
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email.ToLower();
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, model.RoleName);

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new RegisterResponseModel
            {
                Token = await _authService.GenerateToken(user)
            };
        }
    }
}
