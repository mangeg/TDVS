using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Common
{
    public class Helpers
    {
        private static readonly Random _random = new Random();

        #region Random

        public static int RandomNext()
        {
            return _random.Next();
        }
        
        public static int RandomNext(int max)
        {
            return _random.Next(max);
        }

        public static int RandomNext(int min, int max)
        {
            return _random.Next(min, max);
        }

        
        #endregion
    }
}
