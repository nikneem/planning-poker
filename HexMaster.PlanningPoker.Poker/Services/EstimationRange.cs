using System.Collections.Generic;

namespace HexMaster.PlanningPoker.Poker.Services
{
    public static class EstimationRange
    {
        private static List<decimal> _range;

        public static bool InFibonacciRange(this decimal number)
        {
            return _range.Contains(number);
        }

        static EstimationRange()
        {
            _range = new List<decimal> {  .5M, 1, 2, 3, 5, 8, 13, 20, 40, 100};
        }
    }
}
