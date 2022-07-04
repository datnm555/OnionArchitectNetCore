using Newtonsoft.Json;
using OnionArchitect.Service.Exceptions;
using System.Net;

namespace OnionArchitect.Api.Middlerware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await ConvertException(httpContext, ex);
            }
        }

        private Task ConvertException(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            httpContext.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (ex)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception exception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            httpContext.Response.StatusCode = (int)httpStatusCode;

            if (result == String.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }

            return httpContext.Response.WriteAsJsonAsync(result);
        }
    }
}
