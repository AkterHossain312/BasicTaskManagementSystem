using Application.Commands;
using Application.Constant;
using Application.Interface;
using Application.RequestModel;
using Application.ResponseModels;
using AutoMapper;
using Domain.Models.Identity;
using Infrastructure.Constant;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IMediator _iMediator;
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
            IPermissionService permissionService, IMediator iMediator)
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
            _iMediator = iMediator;
        }

    

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginCommand command)
        {
            var user = await _iMediator.Send(command);
            
            return new LoginResponseModel()
            {
                Token = await _authService.GenerateToken(user),
                StatusCode = AppStatusCode.SuccessStatusCode.ToString(),
                Message = MessageConstants.LoginSuccess,
                Permissions = await _permissionService.GetPermissionsByUserId(user.Id),
                UserProfile = await _userService.GetUserById(user.Id)
            };
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterCommand model)
        {
            var user = await _iMediator.Send(model);

            return new RegisterResponseModel
            {
                Token = await _authService.GenerateToken(user)
            };
        }
    }
}
