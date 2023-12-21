using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Interface;
using Application.ResponseModels;
using AutoMapper;
using Domain.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IMapper iMapper, IAuthService authService, IUserService userService, ILogger<RegisterCommandHandler> logger, IPermissionService permissionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = iMapper;
            _logger = logger;
        }

        public async Task<User> Handle(RegisterCommand model, CancellationToken cancellationToken)
        {
            try
            {

                if (await _userManager.Users.AnyAsync(x => x.Email == model.Email.ToLower()))
                { 
                    throw new Exception("Username is taken");
                }
                var user = _mapper.Map<User>(model);
                user.UserName = model.Email.ToLower();
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded) throw new Exception("User or password not found");

                var roleResult = await _userManager.AddToRoleAsync(user, model.RoleName);

                if (!roleResult.Succeeded) throw new Exception("role not found");


                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
