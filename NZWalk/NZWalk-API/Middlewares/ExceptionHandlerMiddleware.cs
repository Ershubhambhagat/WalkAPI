using System.Net;

namespace NZWalk_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> log;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware>  Log,
            RequestDelegate next)
        {
            log = Log;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext) 
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var ErrorId = Guid.NewGuid();
                //Log the Exception 
                log.LogError(ex, $"{ErrorId} : {ex.Message}");

                //Return Custom Error 
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType= "application/json";

                var error = new
                {
                    Id = ErrorId,
                    ErrorMessage = "Something Went Wronge! Wre are looking into resolving this."
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
