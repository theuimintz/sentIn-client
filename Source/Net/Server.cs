using Source.MVVM.Model;
using Source.Net.IO;
using Source.Properties;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Source.Net
{
    /// <summary>
    /// Provides client connection to the remote server
    /// </summary>
    internal class Server
    {

        #region Construction

        public event Action? SignedIn;
        public event Action? LoggedIn;

        public event Action? SignInRejected;
        public event Action? LogInRejected;

        public event Action<List<RoomModel>> RoomFound;
        public event Action<List<RoomModel>>? RoomListReceived;
        public event Action<List<MessageModel>>? MessageListReceived;


        /// <summary>
        /// Provides client connection for TCP Network Services
        /// </summary>
        private readonly TcpClient client;


        /// <summary>
        /// Reads packets from network stream
        /// </summary>
        private PacketReader? pr;


        /// <summary>
        /// Public server's connection hostname
        /// </summary>
        private string hostname;
        public string Hostname { get => hostname; }


        /// <summary>
        /// Public server's connection port
        /// </summary>
        private int port;
        public int Port { get => port; }
        public object SendMEssageListRequest { get; private set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public Server()
        {
            client = new TcpClient();

            hostname = "127.0.0.1";
            port = 4932;
        }

        #endregion

        #region Connecting methods


        /// <summary>
        /// Synchronously makes a connection request
        /// </summary>
        public void ConnectToServer()
        {
            try { client.Connect(Hostname, Port); pr = new PacketReader(client.GetStream()); ReadPackets(); }
            catch (Exception) { }
        }


        /// <summary>
        /// Asynchronously makes a connection requests until the connection is established
        /// </summary>
        public async void StartConnectingToServer()
        {
            await Task.Run(() =>
            {
                while (!client.Connected)
                {
                    try { ConnectToServer(); }
                    catch (Exception) { }
                }
            });
        }


        #endregion

        #region Send Packet Methods

        /// <summary>
        /// Asynchronously sends a sign in request to a remote server
        /// </summary>
        public async void SendSignInRequest(string username, string password, Action<string>? callback = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder packetBuilder = new PacketBuilder();
                    packetBuilder.WriteOpcode(2);

                    packetBuilder.WriteData(username);
                    packetBuilder.WriteData(password);

                    client.Client.Send(packetBuilder.GetPacketBytes());

                    callback?.Invoke("Sign In data has been sent");
                }
                catch (Exception ex)
                {
                    callback?.Invoke(ex.Message);
                }
            });
        }


        /// <summary>
        /// Asynchronously sends a sign up request to a remote server
        /// </summary>
        public async void SendSignUpRequest(string username, string password, Action<string>? callback = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder packetBuilder = new PacketBuilder();
                    packetBuilder.WriteOpcode(1);

                    packetBuilder.WriteData(username);
                    packetBuilder.WriteData(password);

                    client.Client.Send(packetBuilder.GetPacketBytes());

                    callback?.Invoke("Sign Up data has been sent");
                }
                catch (Exception ex)
                {
                    callback?.Invoke(ex.Message);
                }
            });
        }


        /// <summary>
        /// Asynchronously sends room list request to a remote server
        /// </summary>
        public async void SendRoomListRequest(Action<string>? callback = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpcode(159);

                    client.Client.Send(pb.GetPacketBytes());
                }
                catch (Exception ex) { callback?.Invoke(ex.Message); }
            });
        }

        public async void SendRoomCreationRequest(string roomname, Action<string>? callback = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpcode(151);
                    pb.WriteData(roomname);

                    client.Client.Send(pb.GetPacketBytes());
                    callback?.Invoke(string.Empty);
                }
                catch (Exception ex) { callback?.Invoke(ex.Message); }
            });
        }


        /// <summary>
        /// Asynchronously sends message list request to a remote server
        /// </summary>
        public async void SendMessageListRequest(RoomModel room, Action<string>? callback = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();

                    pb.WriteOpcode(109);
                    pb.WriteUID(room.UID);

                    client.Client.Send(pb.GetPacketBytes());
                }
                catch (Exception ex) { callback?.Invoke(ex.Message); }
            });
        }


        /// <summary>
        /// Asynchronously sends message to a remote server
        /// </summary>
        public async void SendMessagePacket(Guid roomUID, string message)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();

                    pb.WriteOpcode(100);

                    pb.WriteUID(roomUID);
                    pb.WriteData(message);

                    client.Client.Send(pb.GetPacketBytes());
                }
                catch (Exception) { }
            });
        }


        /// <summary>
        /// Sends a profile to a remote server
        /// </summary>
        public async void SendProfileRequest()
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();
                    pb.WriteOpcode(5);

                    client.Client.Send(pb.GetPacketBytes());
                }
                catch (Exception) { }
            });
        }


        /// <summary>
        /// Sends the find request built with room's name
        /// </summary>
        /// <param name="name"></param>
        public async void SendRoomToFind(string name)
        {
            await Task.Run(() =>
            {
                try
                {
                    PacketBuilder pb = new PacketBuilder();

                    pb.WriteOpcode(152);
                    pb.WriteData(name);

                    client.Client.Send(pb.GetPacketBytes());
                }
                catch (Exception) { }
            });
        }


        #endregion

        #region Read Packet Methods

        /// <summary>
        /// Synchronously reads packets from networkstream
        /// </summary>
        public void ReadPackets()
        {
            while (true)
            {
                try
                {
                    if (pr == null) throw new NullReferenceException();

                    byte opcode = pr.ReadOpcode(); // Reading opcode from stream

                    switch (opcode)
                    {
                        case 1:
                            ReadSignInResponce();
                            break;
                        case 2:
                            ReadLogInResponce();
                            break;
                        case 5:
                            ReadProfilePacket();
                            break;
                        case 109:
                            ReadMessageList();
                            break;
                        case 152:
                            ReadSearchResponce();
                            break;
                        case 159:
                            ReadRoomList();
                            break;
                    }
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Asynchronously begins reading packets from stream
        /// </summary>
        public async void StartReadingPackets()
        {
            await Task.Run(() =>
            {
                ReadPackets();
            });
        }


        public void ReadSearchResponce()
        {
            try
            {
                if (pr == null) return;

                List<RoomModel> rooms = new List<RoomModel>();

                int len = pr.ReadInteger();
                for (int i = 0; i < len; i++)
                {
                    RoomModel room = pr.ReadRoom();
                    rooms.Add(room);
                }

                RoomFound?.Invoke(rooms);
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Reads sign in responce from stream
        /// </summary>
        private void ReadSignInResponce()
        {
            try
            {
                if (pr == null) return;

                bool res = pr.ReadBoolean();
                if (res)
                {
                    SignedIn?.Invoke();
                    SendProfileRequest();
                }
                else SignInRejected?.Invoke();
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Reads log in responce from stream
        /// </summary>
        private void ReadLogInResponce()
        {
            try
            {
                if (pr == null) return;

                bool res = pr.ReadBoolean();
                if (res)
                {
                    LoggedIn?.Invoke();
                    SendProfileRequest();
                }
                else LogInRejected?.Invoke();
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Reads message list from stream
        /// </summary>
        private void ReadMessageList()
        {
            try
            {

                if (pr == null) return;

                List<MessageModel> messages = new List<MessageModel>();
                int len = pr.ReadInteger();
                for (int i = 0; i < len; i++)
                {
                    messages.Add(pr.ReadMessage());
                }

                MessageListReceived?.Invoke(messages);
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Reads room list from stream
        /// </summary>
        private void ReadRoomList()
        {
            try
            {
                if (pr == null) return;

                List<RoomModel> rooms = new List<RoomModel>();

                int len = pr.ReadInteger();
                for (int i = 0; i < len; i++)
                {
                    RoomModel room = pr.ReadRoom();
                    rooms.Add(room);

                    SendMessageListRequest(room);
                }

                RoomListReceived?.Invoke(rooms);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Reads profile packet
        /// </summary>
        private void ReadProfilePacket()
        {
            if (pr == null) return;

            try
            {
                UserModel user = pr.ReadUser();

                Settings.Default.UID = user.UID;
                Settings.Default.Save();
            }
            catch (Exception) { }
        }

        #endregion

    }
}
