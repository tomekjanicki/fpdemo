namespace Demo.Api
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
