using Source.Properties;
using System;

namespace Source.MVVM.Model
{
    internal class MessageModel : ModelBase
    {
        #region Construction

        /// <summary>
        /// Globally Unique Identifier of a message packet
        /// </summary>
        public Guid UID { get; }


        /// <summary>
        /// The room, where message packet was sent
        /// </summary>
        public RoomModel Room { get; }


        /// <summary>
        /// The Author of a message packet
        /// </summary>
        public UserModel Author { get; }


        /// <summary>
        /// Sending time of a message packet
        /// </summary>
        public DateTime Time { get; }


        /// <summary>
        /// Simple string information of a message packet
        /// </summary>
        public string Message { get; }


        /// <summary>
        /// Represents ownership of a message
        /// </summary>
        public bool IsOwn { get; }


        /// <summary>
        /// Constructor of message model
        /// </summary>
        public MessageModel(Guid uID, RoomModel room, UserModel author, DateTime time, string message)
        {
            UID = uID;
            Room = room;
            Author = author;
            Time = time;
            Message = message;

            IsOwn = Author.UID == Settings.Default.UID;
        }


        #endregion
    }
}
