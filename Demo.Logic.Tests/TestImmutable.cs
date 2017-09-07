namespace Demo.Logic.Tests
{
    using System;
    using System.Collections.Immutable;

    public static class TestImmutable
    {
        public static int Sum(ImmutableList<int> list)
        {
            var sum = 0;
            foreach (var i in list)
            {
                sum = sum + i;
            }
            return sum;
        }

        public static int SumAbsolute(ImmutableList<int> list)
        {
            var sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                //list[i] = Math.Abs(list[i]);
                //sum = sum + list[i];
                sum = sum + Math.Abs(list[i]);
            }
            return sum;
        }
    }
}