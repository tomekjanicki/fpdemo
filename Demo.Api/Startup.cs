namespace Demo.Api
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}