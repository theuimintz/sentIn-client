using System;

namespace Source.MVVM.Model
{
    internal class RoomModel : ModelBase
    {
        #region Construction

        /// <summary>
        /// Global unique identifier of a room model
        /// </summary>
        public Guid UID { get; }


        /// <summary>
        /// The name of a room model
        /// </summary>
        private string name;
        public string Name { get => name; }


        /// <summary>
        /// Last message of a room model
        /// </summary>
        private MessageModel? lastMessage;
        public MessageModel? LastMessage
        {
            get => lastMessage;
            set
            {
                lastMessage = value;
                OnPropertyChanged(nameof(LastMessage));
            }
        }


        /// <summary>
        /// Constructor of Room Model
        /// </summary>
        public RoomModel(Guid uID, string name)
        {
            UID = uID;

            this.name = name;
        }

        #endregion
    }
}
