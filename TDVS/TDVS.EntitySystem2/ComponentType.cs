using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.EntitySystem2
{
    public class ComponentType
    {
        private static long _NextBit = 1;
        private static int _NextId = 0;

        public long Bit { get; private set; }
        public int Id { get; private set; }

        public ComponentType()
        {
            Initialize();
        }

        private void Initialize()
        {
            Bit = _NextBit;
            _NextBit = _NextBit << 1;
            Id = _NextId++;
        }
    }
}
