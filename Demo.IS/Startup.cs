namespace Demo.IS
{
    using System;
    using System.Configuration;
    using System.Security.Cryptography.X509Certificates;
    using Demo.IS.Configuration;
    using IdentityServer3.Core.Configuration;
    using Owin;

    public sealed class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = new IdentityServerServiceFactory()
                           .UseInMemoryUsers(Users.GetUsers())
                           .UseInMemoryClients(Clients.GetClients())
                           .UseInMemoryScopes(Scopes.GetScopes());

            var options = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]), ConfigurationManager.AppSettings["SigningCertificatePassword"]),
                Factory = factory,
                RequireSsl = false
            };

            appBuilder.UseIdentityServer(options);
        }
    }
}