using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestModel
{
    public class PermissionRequestModel
    {
        public int Id { get; set; }
        //  [Required]
        public string PermissionName { get; set; }
        public string Description { get; set; }
    }
}
