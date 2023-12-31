﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestModel
{
    public class RoleRequestModel
    {
        public int? Id { get; set; }
        ///    [Required]
        public string RoleName { get; set; }
        public string[] PermissionList { get; set; }
        public List<PermissionRequestModel> Permissions { get; set; }
    }
}
