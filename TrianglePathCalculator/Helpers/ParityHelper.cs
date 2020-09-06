using TrianglePathCalculator.Common.Enum;

namespace TrianglePathCalculator.Helpers
{
    public static class ParityHelper
    {
        public static bool AreNotEqual(int actual, int expected) 
            => GetParity(actual) != GetParity(expected);

        public static Parity GetParity(int number) 
            => ((number % 2) == 0) ? Parity.Even : Parity.Odd;
    }
}
