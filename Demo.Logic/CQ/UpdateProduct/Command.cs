namespace Demo.Logic.CQ.UpdateProduct
{
    using Demo.Common.Handlers.Interfaces;
    using Demo.Logic.CQ.ValueObjects;
    using Demo.Logic.Shared;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Command : ValueObject<Command>, IRequest<IResult<Error>>
    {
        private Command(PositiveInt id, Name name)
        {
            Id = id;
            Name = name;
        }

        public PositiveInt Id { get; }

        public Name Name { get; }

        public static IResult<Command, NonEmptyString> Create(int id, string name)
        {
            var idResult = PositiveInt.Create(id, (NonEmptyString)nameof(Id));
            var nameResult = Name.Create(name, (NonEmptyString)nameof(Name));

            var result = new IResult<NonEmptyString>[]
            {
                idResult,
                nameResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.IsFailure ? GetFailResult(result.Error) : GetOkResult(new Command(idResult.Value, nameResult.Value));
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
