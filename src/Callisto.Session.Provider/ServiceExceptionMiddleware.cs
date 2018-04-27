using Callisto.SharedKernel;
using Callisto.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="ServiceExceptionMiddleware" />
    /// </summary>
    public class ServiceExceptionMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/></param>
        public ServiceExceptionMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        /// <summary>
        /// Gets the next request delegate in the http pipeline
        /// </summary>
        private RequestDelegate Next { get; }

        /// <summary>
        /// Invoke the middleware component
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/> httpContext</param>
        /// <returns><see cref="Task"/> httpContext</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                var response = RequestResult.Failed($"Oops! Unexpected error occured");
                await httpContext.Response.WriteAsync(response.ToJson( true, SharedKernel.Enum.JsonContractResolver.CamelCase));
            }
        }
    }
}
