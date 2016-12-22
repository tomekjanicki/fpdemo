namespace Demo.Common.Api.Infrastructure
{
    using System;
    using System.Collections.Immutable;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using NullGuard;

    public sealed class ImmutableListModelBinderProvider : ModelBinderProvider
    {
        [return: AllowNull]
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (modelType.IsGenericType && modelType.GetGenericTypeDefinition() == typeof(ImmutableList<>))
            {
                return (IModelBinder)Activator.CreateInstance(typeof(ImmutableListModelBinder<>).MakeGenericType(modelType.GenericTypeArguments));
            }

            return null;
        }
    }
}