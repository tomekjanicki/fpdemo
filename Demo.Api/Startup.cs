namespace Demo.Api
{
    using System.Web.Http;

    using IdentityServer3.AccessTokenValidation;
    using Owin;

    public sealed class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            RegisterContainer.Execute(httpConfiguration);
            RegisterSwagger.Execute(httpConfiguration);
            RegisterWebApiRoutes.Execute(httpConfiguration);
            RegisterWebApiMiscs.Execute(httpConfiguration);

            appBuilder.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:3470"
            });

            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}