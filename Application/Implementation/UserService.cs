using Application.Constant;
using Application.Interface;
using Application.ResponseModels;
using AutoMapper;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserResponseModel> GetUserById(int userId)
        {
            var user = await _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NullReferenceException(MessageConstants.UserNotFound);
            }

            var userViewModel = _mapper.Map<UserResponseModel>(user);
            return userViewModel;
        }
    }
}
