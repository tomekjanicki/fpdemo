﻿namespace Demo.Logic.CQ.UpdateProduct
{
    using System.Collections.Immutable;
    using Demo.Common.Handlers.Interfaces;
    using Demo.Common.Shared;
    using Demo.Logic.CQ.ValueObjects;
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

        public static IResult<Command, NonEmptyString> TryCreate(int id, string name, int? size)
        {
            var idResult = PositiveInt.TryCreate(id, (NonEmptyString)nameof(Id));
            var nameResult = Name.TryCreate(name, (NonEmptyString)nameof(Name));
            var sizeResult = PositiveInt.TryCreate(size, (NonEmptyString)nameof(Size));

            var result = new IResult<NonEmptyString>[]
            {
                idResult,
                nameResult,
                sizeResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(idResult.Value, nameResult.Value, sizeResult.Value)));
        }

        protected override bool EqualsCore(Command other)
        {
            return Id == other.Id && Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Id, Name }.ToImmutableList());
        }
    }
}
