using System.Text.Json;
using System.Net;

namespace HelpDeskAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> logger)
        {
            _Next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new {message = "An unexpected error occured.", detail = ex.Message};
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}