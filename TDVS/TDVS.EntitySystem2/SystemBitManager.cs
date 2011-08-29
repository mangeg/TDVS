using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem2
{
    public static class SystemBitManager
    {
        private static int POS = 0;
        private static Dictionary<EntitySystem, long> _SystemBits = new Dictionary<EntitySystem, long>();

        public static long GetBitFor(EntitySystem es)
        {
            long bit;
            var hasBit = _SystemBits.TryGetValue(es, out bit);
            if(!hasBit)
            {
                bit = 1L << POS;
                POS++;
                _SystemBits.Add(es, bit);
            }

            return bit;
        }
    }
}
