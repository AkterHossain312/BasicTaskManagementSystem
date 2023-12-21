using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;

namespace Application.ResponseModels
{
    public class ApiResponse
    {
        public ApiResponse(string message)
        {
            Message = message;
        }
        public ApiResponse(string message, dynamic data)
        {
            Message = message;
            Data = data;
        }
        public ApiResponse(string message, ResponseCode code)
        {
            Message = message;
            StatusCode = code;
        }
        public ApiResponse(string message, dynamic data, ResponseCode code)
        {
            Message = message;
            Data = data;
            StatusCode = code;
        }
        public string Message { get; set; }
        public ResponseCode StatusCode { get; set; }
        public dynamic Data { get; set; }
    }
}
