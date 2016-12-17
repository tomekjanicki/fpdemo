namespace Demo.Types
{
    using Demo.Types.FunctionalExtensions;
    using NullGuard;

    public static class StringExtensions
    {
        public static string IfNullReplaceWithEmptyString(this Maybe<string> input)
        {
            return input.HasNoValue ? string.Empty : input.Value;
        }

        public static string IfNullReplaceWithEmptyString([AllowNull]this string input)
        {
            return input ?? string.Empty;
        }
    }
}
