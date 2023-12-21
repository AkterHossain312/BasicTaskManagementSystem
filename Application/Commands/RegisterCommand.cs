using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ResponseModels;
using Domain.Models.Identity;
using MediatR;

namespace Application.Commands
{
    public class RegisterCommand : IRequest<User>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
