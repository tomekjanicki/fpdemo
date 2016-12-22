﻿namespace Demo.Api
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Demo.Common.Api.Infrastructure;
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

            configuration.Services.Insert(typeof(ModelBinderProvider), 0, new ImmutableListModelBinderProvider());
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