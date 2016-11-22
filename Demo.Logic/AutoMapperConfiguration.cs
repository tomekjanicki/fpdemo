namespace Demo.Logic
{
    using AutoMapper;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<CQ.GetProductById.Product, Dtos.Apis.Product.Get.Product>();
        }
    }
}
