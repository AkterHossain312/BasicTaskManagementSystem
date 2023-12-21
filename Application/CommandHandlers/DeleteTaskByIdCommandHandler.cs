using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Constant;
using Application.Enums;
using Application.ResponseModels;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers
{
    public class DeleteTaskByIdCommandHandler : IRequestHandler<DeleteTaskByIdCommand, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private TaskManagementDbContext _context;

        public DeleteTaskByIdCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, TaskManagementDbContext context)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ApiResponse> Handle(DeleteTaskByIdCommand request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault()?.Value);
            var taskInfo = await _context.Tasks.Where(x => x.Id == request.Id && x.UserId == userId)
                .FirstOrDefaultAsync();
            
            if (taskInfo != null)
            {
                 _context.Tasks.Remove(taskInfo);

                 await _context.SaveChangesAsync();
                 return new ApiResponse(MessageConstants.DeleteSuccess, ResponseCode.Success);
            }

            return new ApiResponse(MessageConstants.DeleteFailed, ResponseCode.Failed);
        }
    }
}
