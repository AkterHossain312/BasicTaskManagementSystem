using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ResponseModels;
using MediatR;

namespace Application.Commands
{
    public class DeleteTaskByIdCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
