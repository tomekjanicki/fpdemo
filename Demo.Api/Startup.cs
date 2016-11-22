namespace Demo.Api
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            RegisterContainer.Execute(httpConfiguration);
            RegisterSwagger.Execute(httpConfiguration);
            RegisterWebApiRoutes.Execute(httpConfiguration);
            RegisterWebApiMiscs.Execute(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}