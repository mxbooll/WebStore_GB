using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore_GB.Infrastructure.Midleware
{
    public class ErrorHandlingMidleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMidleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
