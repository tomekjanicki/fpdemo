namespace Demo.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Demo.Common.Api.Infrastructure.Security.Interfaces;
    using Demo.Types;
    using Swashbuckle.Application;
    using Swashbuckle.Swagger;

    public static class RegisterSwagger
    {
        public static void Execute(HttpConfiguration configuration)
        {
            const string versionString = "v1_0";
            var accessConfigurationMapProvider = (IAccessConfigurationMapProvider)configuration.DependencyResolver.GetService(typeof(IAccessConfigurationMapProvider));
            configuration.EnableSwagger(config => Configure(versionString, config, accessConfigurationMapProvider)).EnableSwaggerUi(ConfigureUi);
        }

        private static void Configure(string version, SwaggerDocsConfig config, IAccessConfigurationMapProvider accessConfigurationMapProvider)
        {
            config.SingleApiVersion(version, "Demo.Api");
            config.UseFullTypeNameInSchemaIds();
            config.OperationFilter(() => new AssignOAuth2SecurityRequirements(accessConfigurationMapProvider));
            config
              .OAuth2("oauth2")
              .Description("OAuth2 Resource Owner")
              .Flow("resourceOwner")
              .AuthorizationUrl("http://localhost:3470/connect/authorize")
              .TokenUrl("http://localhost:3470/connect/token")
              .Scopes(scopes =>
              {
                  scopes.Add("write", "Write user data");
              });
        }

        private static void ConfigureUi(SwaggerUiConfig config)
        {
            config.DocExpansion(DocExpansion.List);
            config.DisableValidator();
        }

        private class AssignOAuth2SecurityRequirements : IOperationFilter
        {
            private readonly IAccessConfigurationMapProvider _accessConfigurationMapProvider;

            public AssignOAuth2SecurityRequirements(IAccessConfigurationMapProvider accessConfigurationMapProvider)
            {
                _accessConfigurationMapProvider = accessConfigurationMapProvider;
            }

            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                var map = _accessConfigurationMapProvider.Get();

                var actionWithResource = (NonEmptyLowerCaseString)operation.operationId.Replace('_', '/');

                if (map.ContainsKey(actionWithResource))
                {
                    var scopes = map[actionWithResource];

                    if (scopes.Anonymous)
                    {
                        return;
                    }

                    InitSecurityNodeIfNull(operation);

                    operation.security.Add(GetOAuthRequirements(scopes.ScopeCollection.Select(s => s.Value)));
                }
                else
                {
                    InitSecurityNodeIfNull(operation);

                    operation.security.Add(GetOAuthRequirements(Enumerable.Empty<string>()));
                }
            }

            private static Dictionary<string, IEnumerable<string>> GetOAuthRequirements(IEnumerable<string> scopes)
            {
                return new Dictionary<string, IEnumerable<string>> { { "oauth2", scopes } };
            }

            private static void InitSecurityNodeIfNull(Operation operation)
            {
                if (operation.security == null)
                {
                    operation.security = new List<IDictionary<string, IEnumerable<string>>>();
                }
            }
        }
    }
}