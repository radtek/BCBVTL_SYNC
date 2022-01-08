using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Sync.Api.CustomAttr
{
    public class CustomModule : ActionFilterAttribute
    {
        public string Module { get; set; } 
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var configModule = ConfigurationSettings.AppSettings.Get("Module");
            if (Module != configModule)
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}