using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Constant;
using Application.Interface;
using Application.ResponseModels;
using AutoMapper;
using Domain.Models.Identity;
using Infrastructure.Constant;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _iMapper;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IPermissionService _permissionService;

        public LoginCommandHandler(IMapper iMapper, IUserService userService, IAuthService authService, ILogger<LoginCommandHandler> logger, SignInManager<User> signInManager, UserManager<User> userManager, IPermissionService permissionService)
        {
            _iMapper = iMapper;
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _permissionService = permissionService;
        }

        public async Task<User> Handle(LoginCommand model, CancellationToken cancellationToken)
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
                return user;
            }

            throw new UnAuthorizedException(MessageConstants.UsernamePasswordDoNotMatch);
        }
    }
}
