using System;
using System.IO;
using System.Text;

namespace Source.Net.IO
{
    internal class PacketBuilder
    {
        #region Construction

        private MemoryStream ms;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PacketBuilder()
        {
            ms = new MemoryStream();
        }

        #endregion


        #region Writing Data Methods

        /// <summary>
        /// Writes packet opcode
        /// </summary>
        /// <param name="opcode">Packet opcode</param>
        public void WriteOpcode(byte opcode)
        {
            ms.WriteByte(opcode);
        }


        /// <summary>
        /// Writes packet string data
        /// </summary>
        /// <param name="data">Packet string data</param>
        public void WriteData(string data)
        {
            int len = data.Length;

            ms.Write(BitConverter.GetBytes(len));
            ms.Write(Encoding.ASCII.GetBytes(data));
        }


        /// <summary>
        /// Writes integer to memory stream
        /// </summary>
        public void WriteInteger(int data)
        {
            ms.Write(BitConverter.GetBytes(data));
        }


        /// <summary>
        /// Writes prepared bytes to memory stream
        /// </summary>
        /// <param name="data"></param>
        public void WriteBytes(byte[] data)
        {
            ms.Write(data);
        }


        public void WriteUID(Guid uID)
        {
            WriteData(uID.ToString());
        }

        #endregion

        /// <summary>
        /// GetPacketBytes method
        /// </summary>
        public byte[] GetPacketBytes()
        {
            return ms.ToArray();
        }

    }
}
