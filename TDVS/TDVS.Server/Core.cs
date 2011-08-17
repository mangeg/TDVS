using System;
using System.Collections.Generic;
using Lidgren.Network;
using TDVS.Common.Network;


namespace TDVS.Server
{
    public class Core
    {
        public NetServer Server;
        public NetPeerConfiguration Configuration;
        public List<PlayerState> GameWorldState = new List<PlayerState>();

        private NetIncomingMessage _IncommingMessage;
        private DateTime _Time;
        private TimeSpan _TimeToPass;

        public void Initialize(int port, int maxConnections)
        {
            Configuration = new NetPeerConfiguration(Common.Settings.APPLICATION_NAME)
                                {
                                    Port = port,
                                    MaximumConnections = maxConnections
                                };

            Configuration.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Server = new NetServer(Configuration);
            Server.Start();

            Console.WriteLine("Server started");

            _Time = DateTime.Now;
            _TimeToPass =  new TimeSpan(0, 0, 0, 0, 30);

            Console.WriteLine("Waiting for new connections and updating world state to current ones");
        }

        public void Run()
        {
            while((_IncommingMessage = Server.ReadMessage()) != null)
            {
                switch(_IncommingMessage.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:

                        // Read the first byte, it should _Always_ be a byte with the PacketType.
                        var packetType = (PacketType) _IncommingMessage.ReadByte();

                        // If we get a login packet...
                        if(packetType == PacketType.Login)
                        {
                            // ...approve the login attempt immedietly.
                            _IncommingMessage.SenderConnection.Approve();

                            var name = _IncommingMessage.ReadString();
                            var playerState = new PlayerState(name: name,
                                                              networkId:
                                                                  _IncommingMessage.SenderConnection.
                                                                  RemoteUniqueIdentifier,
                                                              x: NetRandom.Instance.Next(100, 200),
                                                              y: NetRandom.Instance.Next(100, 200),
                                                              connection: _IncommingMessage.SenderConnection);
                            GameWorldState.Add(playerState);

                            var outgoingMessage = Server.CreateMessage();
                            outgoingMessage.Write((byte) PacketType.Worldstate);
                            outgoingMessage.Write(GameWorldState.Count);

                            foreach(var player in GameWorldState)
                            {
                                outgoingMessage.WriteAllProperties(player);
                            }

                            Server.SendMessage(outgoingMessage, _IncommingMessage.SenderConnection,
                                               NetDeliveryMethod.ReliableOrdered, 0);

                            Console.WriteLine("Approved new connection and updated the world status");
                        }

                        break;
                }
            }
        }

    }
}
