namespace Demo.Logic.Tests
{
    using System;
    using System.Collections.Generic;

    public static class TestMutable
    {        
        public static int Sum(List<int> list)
        {
            var sum = 0;
            foreach (var i in list)
            {
                sum = sum + i;
            }
            return sum;
        }

        public static int SumAbsolute(List<int> list)
        {
            var sum = 0;
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = Math.Abs(list[i]);
                sum = sum + list[i];
            }
            return sum;
        }
    }
}