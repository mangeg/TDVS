using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Network.Packets
{
    public interface IBasePacket
    {
        NetOutgoingMessage Encode(NetOutgoingMessage outgoingMessage);
        BasePacket Decode(NetIncomingMessage incomingMessage);
    }

    public abstract class BasePacket : IBasePacket
    {
        public static PacketType Type;

        public abstract NetOutgoingMessage Encode(NetOutgoingMessage outgoingMessage);
        public abstract BasePacket Decode(NetIncomingMessage incomingMessage);
    }
}
