using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Implementation;
using Application.Queries;
using Application.ResponseModels;
using Application.ViewModels;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.QueryHandlers
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTaskQuery, PagedResponse<TaskViewModel>>
    {
        private readonly TaskManagementDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllTasksQueryHandler(IHttpContextAccessor httpContextAccessor, TaskManagementDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<PagedResponse<TaskViewModel>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            int total = 0;
            
            var TaskList = await _context.Tasks
                .Where(a => a.Title.Contains(request.SearchText ?? ""))
                .OrderBy(x => x.Title)
                .Skip(request.ToPagination().ToSkip())
                .Take(request.ToPagination().ToTake())
                .ToListAsync(cancellationToken);

            total = await _context.Tasks.CountAsync(cancellationToken);

            var result = TaskList
                .Select(y => new TaskViewModel()
                {
                    
                    Title = y.Title,
                    Description = y.Description,
                    Status = y.Status,
                    DueDate = y.DueDate
                }).ToList();

            
            return new PagedResponse<TaskViewModel>(result, Pagination.FromQuery(total, request.PageNo, request.PageSize));
        }
    }
}
