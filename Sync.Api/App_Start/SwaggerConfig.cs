using System.Web.Http;
using WebActivatorEx;
using Sync.Api;
using Swashbuckle.Application;
using System.Linq;
using Swashbuckle.Examples;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Sync.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger((c) =>
                {
                    c.SingleApiVersion("v1", "Sync API");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.IncludeXmlComments(string.Format(@"{0}\bin\Sync.Api.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                    c.OperationFilter<ExamplesOperationFilter>();
                })
              .EnableSwaggerUi();
        }
    }
}
