using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDVS.Common.Network.Packets
{
    public class ServerPacket
    {
        public static PacketType Type = PacketType.Server;
        public int X;
        public int Y;
    }
}
