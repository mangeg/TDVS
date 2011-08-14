using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace TDVS.Common.Network
{
    public class PlayerState
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public string Name { get; set; }
        public long NetworkId { get; set; }
        public NetConnection Connection { get; set; }
        
        public PlayerState() {}

        public PlayerState(string name, long networkId, int x, int y, NetConnection connection)
        {
            Name = name;
            X = x;
            Y = y;
            Connection = Connection;
            NetworkId = networkId;
        }
    }
}
