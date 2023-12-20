using Infrastructure.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string msg) : base(msg)
        {

        }

        public virtual int ToHttpStatusCode()
        {
            return AppStatusCode.BadRequestStatusCode;
        }
    }
}
