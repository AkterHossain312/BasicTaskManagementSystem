using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumerations
{
    public enum StatusEnum
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("InProgress")]
        InProgress = 3
        
    }
}
