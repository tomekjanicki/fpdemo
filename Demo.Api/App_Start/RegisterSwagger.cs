namespace Demo.Api
{
    using System.Web.Http;
    using Swashbuckle.Application;

    public static class RegisterSwagger
    {
        public static void Execute(HttpConfiguration configuration)
        {
            const string versionString = "v1_0";
            configuration.EnableSwagger(config => Configure(versionString, config)).EnableSwaggerUi(ConfigureUi);
        }

        private static void Configure(string version, SwaggerDocsConfig config)
        {
            config.SingleApiVersion(version, "Demo.Api");
            config.UseFullTypeNameInSchemaIds();
        }

        private static void ConfigureUi(SwaggerUiConfig config)
        {
            config.DocExpansion(DocExpansion.List);
            config.DisableValidator();
        }
    }
}