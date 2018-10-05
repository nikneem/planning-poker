using System;
using System.Text;

namespace HexMaster.ScrumPoker.Api.Services
{
    public static class Randomizer
    {

        private static Random _rnd;
        private static string _pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0192837465";

        public static string GetRandomRefinementCode()
        {
            var code = new StringBuilder();
            while (code.Length < 8)
            {
                code.Append(_pool.Substring(_rnd.Next(0, _pool.Length), 1));
            }

            return code.ToString();
        }

         static Randomizer()
        {
            _rnd= new Random();
        }
    }
}
