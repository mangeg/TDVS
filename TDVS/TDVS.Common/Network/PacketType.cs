using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace TDVS.Common.Network
{
    public enum PacketType
    {
        Error = 0,
        Player = 1 << 0,
        Server = 1 << 1,
        Login = 1 << 2,
        Worldstate = 1 << 3,
        TileMap = 1 << 4
    }
}
