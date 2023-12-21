using Application.ResponseModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Application.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<User>;
}
