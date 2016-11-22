namespace Demo.Api
{
    using System.Web.Http;

    public static class RegisterWebApiRoutes
    {
        public static void Execute(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
