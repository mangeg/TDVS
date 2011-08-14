using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace TDVS.Common.Network.Packets
{
    public class PlayerPacket : BasePacket
    {
        public long Id { get; set; }
        public string PlayerName { get; set; }
        public Color Color { get; set; }
        public Vector2 Position { get; set; }

        public PlayerPacket() { Type = PacketType.Player; }

        public override NetOutgoingMessage Encode(NetOutgoingMessage outgoingMessage)
        {
            var serialized = Common.Serialization.ObjectToByteArray(this);

            outgoingMessage.Write((byte) Type);
            outgoingMessage.Write(serialized.Length);
            outgoingMessage.Write(serialized);
            


            /*outgoingMessage.Write((byte)Type);

            outgoingMessage.Write(Id);
            outgoingMessage.Write(PlayerName);

            outgoingMessage.Write(Color.R);
            outgoingMessage.Write(Color.G);
            outgoingMessage.Write(Color.B);

            outgoingMessage.Write(Position.X);
            outgoingMessage.Write(Position.Y);*/

            return outgoingMessage;
        }

        public override BasePacket Decode(NetIncomingMessage incomingMessage)
        {
            var length = incomingMessage.ReadInt32();
            var bytes = incomingMessage.ReadBytes(length);
            var playerPacket = Common.Serialization.ByteArrayToObject<PlayerPacket>(bytes);


            /*playerPacket.Id = incomingMessage.ReadInt64();
            playerPacket.PlayerName = incomingMessage.ReadString();

            var r = incomingMessage.ReadByte();
            var g = incomingMessage.ReadByte();
            var b = incomingMessage.ReadByte();
            playerPacket.Color = new Color(r, g, b);

            var x = incomingMessage.ReadFloat();
            var y = incomingMessage.ReadFloat();
            playerPacket.Position = new Vector2(x, y);*/


            return playerPacket;
        }

        public PlayerPacket DecodePlayerPacket(NetIncomingMessage incomingMessage)
        {
            return (PlayerPacket)Decode(incomingMessage);
        }
    }
}
