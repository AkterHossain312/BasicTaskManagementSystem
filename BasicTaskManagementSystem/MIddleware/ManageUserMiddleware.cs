using Infrastructure.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace WebApi.MIddleware
{
    public class ManageUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManageUserMiddleware> _logger;

        public ManageUserMiddleware(RequestDelegate next, ILogger<ManageUserMiddleware> logger)
        {
            this._next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogError(ex.Message + ex.StackTrace, null);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                DomainException domEx => (HttpStatusCode)domEx.ToHttpStatusCode(),
                UnAuthorizedException unAuthEx => (HttpStatusCode)unAuthEx.ToHttpStatusCode(),
                ConflictException conflictEx => (HttpStatusCode)conflictEx.ToHttpStatusCode(),
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(new
            {
                message = ex.Message ?? string.Empty,
                source = ex.Source,
                innerException = ex.InnerException?.Message ?? string.Empty,
                statusCode = (int)code
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}