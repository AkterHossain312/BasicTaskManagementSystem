using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging
{
    public interface IDbContextService
    {
        FrameworkDbContext GetContext();
    }
}
