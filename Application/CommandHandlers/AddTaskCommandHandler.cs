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
using Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CommandHandlers
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private TaskManagementDbContext _context;

        public AddTaskCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, TaskManagementDbContext context)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ApiResponse> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault()?.Value);
            var taskInfo = _mapper.Map<Tasks>(request);

            if (taskInfo == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            taskInfo.UserId = userId;

            await _context.AddAsync(taskInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            await SaveDataToUserTaskTable(taskInfo, cancellationToken);


            return new ApiResponse(MessageConstants.SaveSuccess, ResponseCode.Success); ;
        }

        private async Task SaveDataToUserTaskTable(Tasks taskInfo, CancellationToken cancellationToken)
        {
            
            var userTask = new UserTaskCommand
            {
                TaskId = taskInfo.Id,
                UserId = taskInfo.UserId,
            };

            var UserTaskInfo = _mapper.Map<UserTask>(userTask);


            await _context.AddAsync(UserTaskInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
