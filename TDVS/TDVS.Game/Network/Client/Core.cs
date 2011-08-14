using System;
using System.Collections.Generic;
using Lidgren.Network;
using TDVS.Common.Network;

namespace TDVS.Game.Network.Client
{
    public class Core
    {
        public List<PlayerState> GameWorldState;

        public NetClient Client;
        public NetPeerConfiguration Configuration;
        private NetOutgoingMessage _OutGoingMessage;

        public string Ip = "127.0.0.1";
        public int Port = 40404;

        public void Initialize()
        {
            GameWorldState = new List<PlayerState>();

            Configuration = new NetPeerConfiguration(Common.Settings.APPLICATION_NAME);
            Client = new NetClient(Configuration);

            _OutGoingMessage = Client.CreateMessage();
            
            Client.Start();

            _OutGoingMessage.Write((byte) PacketType.Login);
            _OutGoingMessage.Write("blueblood");

            Client.Connect(Ip, Port, _OutGoingMessage);
            Console.WriteLine("Client started");

            WaitForApproval();
        }

        private void WaitForApproval()
        {
            var readyForAction = false;
            NetIncomingMessage incomingMessage;

            while(!readyForAction)
            {
                if((incomingMessage = Client.ReadMessage()) != null)
                {
                    switch(incomingMessage.MessageType)
                    {
                        case NetIncomingMessageType.Data:

                            var packetType = (PacketType) incomingMessage.ReadByte();

                            if(packetType == PacketType.Worldstate)
                            {
                                GameWorldState.Clear();

                                var count = 0;
                                count = incomingMessage.ReadInt32();

                                for(var i = 0; i < count; i++)
                                {
                                    var player = new PlayerState();

                                    incomingMessage.ReadAllProperties(player);

                                    GameWorldState.Add(player);
                                }

                                readyForAction = true;
                            }
                            break;

                        default:
                            Console.WriteLine(incomingMessage.ReadString() + " strange message");
                            break;
                    }
                }
            }
        }
    }
}
