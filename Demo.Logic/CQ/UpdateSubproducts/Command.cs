namespace Demo.Logic.CQ.UpdateSubproducts
{
    using System.Collections.Generic;
    using System.Linq;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<Error>>
    {
        private Command(PositiveInt id, IReadOnlyCollection<PositiveInt> subProductIds)
        {
            Id = id;
            SubProductIds = subProductIds;
        }

        public PositiveInt Id { get; }

        public IReadOnlyCollection<PositiveInt> SubProductIds { get; }

        public static IResult<Command, NonEmptyString> Create(int id, IReadOnlyCollection<int> subProductIds)
        {
            var idResult = PositiveInt.Create(id, (NonEmptyString)nameof(Id));

            var subProductListResult = SubProductListCreate(subProductIds);

            var result = new IResult<NonEmptyString>[]
            {
                idResult,
                subProductListResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(idResult.Value, subProductListResult.Value)));
        }

        protected override bool EqualsCore(Command other)
        {
            return Id == other.Id && SubProductIds.Equals(other.SubProductIds);
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { Id, SubProductIds });
        }

        private static IResult<IReadOnlyCollection<PositiveInt>, NonEmptyString> SubProductListCreate(IReadOnlyCollection<int> subProductIds)
        {
            if (subProductIds.Count == 0)
            {
                return ((NonEmptyString)(nameof(SubProductIds) + " cannot be empty")).GetFailResult<IReadOnlyCollection<PositiveInt>>();
            }

            var subProductIdList = new List<PositiveInt>();

            foreach (var subProductIdResult in subProductIds.Select(subProductId => PositiveInt.Create(subProductId, (NonEmptyString)nameof(SubProductIds))))
            {
                if (subProductIdResult.IsFailure)
                {
                    return subProductIdResult.Error.GetFailResult<IReadOnlyCollection<PositiveInt>>();
                }

                subProductIdList.Add(subProductIdResult.Value);
            }

            return subProductIdList.Distinct().ToList().GetOkMessage();
        }
    }
}