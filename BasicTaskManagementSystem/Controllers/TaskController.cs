using Application.Queries;
using Application.ResponseModels;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    public class TaskController : BaseController
    {
        public readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("GetAllTasks")]
        public async Task<PagedResponse<TaskViewModel>> GetAllUsers([FromQuery] GetAllTaskQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
