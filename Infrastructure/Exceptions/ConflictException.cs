﻿using Infrastructure.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }

        public virtual int ToHttpStatusCode()
        {
            return AppStatusCode.Conflict;
        }
    }
}