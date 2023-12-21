using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Constant;
using Application.Enums;
using Application.ResponseModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private TaskManagementDbContext _context;

        public UpdateTaskCommandHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, TaskManagementDbContext context)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ApiResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateTaskInfo = _mapper.Map<Tasks>(request);
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault()?.Value);
                var isExist = await _context.Tasks.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId,
                        cancellationToken: cancellationToken);

                if (isExist != null)
                {
                    updateTaskInfo.CreatedBy = isExist.CreatedBy;
                    updateTaskInfo.UpdatedBy = userId;
                    updateTaskInfo.UserId = userId;

                    _context.Update(updateTaskInfo);
                    
                    await _context.SaveChangesAsync(cancellationToken);
                    return new ApiResponse(MessageConstants.UpdateSuccess, ResponseCode.Success);

                }


                return new ApiResponse(MessageConstants.UpdateFailed, ResponseCode.Failed);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
