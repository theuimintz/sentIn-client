using Source.MVVM.Model;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Source.Net.IO
{
    internal class PacketReader : BinaryReader
    {

        #region Construction

        private NetworkStream ns;

        public PacketReader(NetworkStream ns) : base(ns)
        {
            this.ns = ns;
        }

        #endregion

        #region Reading Data

        /// <summary>
        /// Reads Opcode from NetworkStream
        /// </summary>
        public byte ReadOpcode()
        {
            return ReadByte();
        }


        /// <summary>
        /// Reads string data from NetworkStream
        /// </summary>
        public string ReadData()
        {
            int len = ReadInt32();
            byte[] buffer = new byte[len];
            ns.Read(buffer, 0, len);

            return Encoding.ASCII.GetString(buffer);
        }


        /// <summary>
        /// Reads Integer from NetworkStream
        /// </summary>
        public int ReadInteger()
        {
            return ReadInt32();
        }


        /// <summary>
        /// Reads RoomModel packet from NetworkStream
        /// </summary>
        public RoomModel ReadRoom()
        {
            return new RoomModel(ReadUID(), ReadData());
        }


        /// <summary>
        /// Reads user model from the stream
        /// </summary>
        public UserModel ReadUser()
        {
            return new UserModel(ReadUID(), ReadData(), ReadData());
        }


        /// <summary>
        /// Reads DateTime from the stream
        /// </summary>
        public DateTime ReadDateTime()
        {
            return DateTime.FromBinary(ReadInt64());
        }


        /// <summary>
        /// Reads message model from the stream
        /// </summary>
        public MessageModel ReadMessage()
        {
            return new MessageModel(ReadUID(), ReadRoom(), ReadUser(), ReadDateTime(), ReadData());
        }


        /// <summary>
        /// Reads globally unique identifier from the stream
        /// </summary>
        public Guid ReadUID()
        {
            return Guid.Parse(ReadData());
        }


        #endregion

    }
}
