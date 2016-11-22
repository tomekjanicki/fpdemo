namespace Demo.Logic.CQ.UpdateProduct
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Logic.CQ.ValueObjects;
    using Demo.Logic.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<Error>>
    {
        private Command(PositiveInt id, Name name, PositiveInt size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public PositiveInt Id { get; }

        public Name Name { get; }

        public PositiveInt Size { get; }

        public static IResult<Command, NonEmptyString> Create(int id, string name, int? size)
        {
            var idResult = PositiveInt.Create(id, (NonEmptyString)nameof(Id));
            var nameResult = Name.Create(name, (NonEmptyString)nameof(Name));
            var sizeResult = PositiveInt.Create(size, (NonEmptyString)nameof(Size));

            var result = new IResult<NonEmptyString>[]
            {
                idResult,
                nameResult,
                sizeResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Command(idResult.Value, nameResult.Value, sizeResult.Value));
        }

        protected override bool EqualsCore(Command other)
        {
            return Id == other.Id && Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Id, Name });
        }
    }
}
