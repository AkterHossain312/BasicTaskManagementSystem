using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum ResponseCode
    {
        Success = 200,
        Failed = 400,
        AlreadyExist = 409,
        Error = 500,
    }
}
