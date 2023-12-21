using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Domain.Models
{
    public class Tasks : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusEnum Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
