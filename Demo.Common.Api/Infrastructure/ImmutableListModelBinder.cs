namespace Demo.Common.Api.Infrastructure
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Web.Http.Controllers;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.ModelBinding.Binders;

    public sealed class ImmutableListModelBinder<T> : CollectionModelBinder<T>
    {
        protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<T> newCollection)
        {
            bindingContext.Model = newCollection.ToImmutableList();
            return true;
        }
    }
}