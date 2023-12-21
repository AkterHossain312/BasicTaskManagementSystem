using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Models;

namespace Application.Mapping
{
    public class TaskMappingProfile : MappingProfile
    {
        public TaskMappingProfile()
        {
            CreateMap<AddTaskCommand, Tasks>();
            CreateMap<UpdateTaskCommand, Tasks>();
            CreateMap<DeleteTaskByIdCommand, Tasks>();
        }
    }
}
