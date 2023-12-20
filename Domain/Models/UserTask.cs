using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Identity;

namespace Domain.Models
{
    public class UserTask : BaseModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public User User { get; set; }
        public Tasks Tasks { get; set; }
    }
}
