using System;

namespace HuntTheWumpus
{
    static class RNG
    {
        private static Random _random = new Random();

        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            return _random.Next(minimumValue, maximumValue);
        }
    }
}
