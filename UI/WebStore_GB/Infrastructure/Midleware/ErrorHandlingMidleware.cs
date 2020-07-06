using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebStore_GB.Infrastructure.Midleware
{
    public class ErrorHandlingMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMidleware> _logger;

        public ErrorHandlingMidleware(RequestDelegate next, ILogger<ErrorHandlingMidleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
                throw;
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Ошибка при обработке запроса {0}", context.Request.Path);
        }
    }
}
