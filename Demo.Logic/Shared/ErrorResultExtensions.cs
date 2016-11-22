namespace Demo.Logic.Shared
{
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static IResult<Error> ToGeneric(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToGeneric<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToNotFound<T>()
        {
            return Result<T, Error>.Fail(Error.CreateNotFound());
        }

        public static IResult<Error> ToNotFound()
        {
            return Result<Error>.Fail(Error.CreateNotFound());
        }
    }
}
