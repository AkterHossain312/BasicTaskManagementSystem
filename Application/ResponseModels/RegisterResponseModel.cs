﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseModels
{
    public class RegisterResponseModel
    {
        public string Token { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Permissions { get; set; }
    }
}