using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ResponseModels;
using Domain.Enumerations;
using MediatR;

namespace Application.Commands
{
    public class AddTaskCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}
