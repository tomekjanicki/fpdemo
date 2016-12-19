namespace Demo.Api
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Thinktecture.IdentityModel.WebApi;

    public static class RegisterWebApiMiscs
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.Formatters.Clear();
            configuration.Formatters.Add(GetConfiguredJsonMediaTypeFormatter());
            configuration.Filters.Add(new ResourceActionAuthorizeAttribute());
        }

        private static JsonMediaTypeFormatter GetConfiguredJsonMediaTypeFormatter()
        {
            var result = new JsonMediaTypeFormatter();
            var mediaTypeHeaderValue = result.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "text/json");
            if (mediaTypeHeaderValue != null)
            {
                result.SupportedMediaTypes.Remove(mediaTypeHeaderValue);
            }

            result.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            result.SerializerSettings.Converters.Add(new StringEnumConverter());
            return result;
        }
    }
}