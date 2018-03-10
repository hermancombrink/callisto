using Callisto.SharedKernal.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Callisto.Web.Client
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServiceOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailOptions>(config.GetSection("SendGrid"));
        }
    }
}
