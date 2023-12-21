using Application.Commands;
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

        [Authorize]
        [HttpPost("CreateTask")]
        public async Task<ActionResult> Create([FromBody] AddTaskCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("UpdateTask")]
        public async Task<ActionResult> Update([FromBody] UpdateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("DeleteTask/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTaskByIdCommand {Id = id} );

            return Ok(result);
        }
    }
}
