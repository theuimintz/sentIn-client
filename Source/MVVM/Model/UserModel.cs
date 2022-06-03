using System;

namespace Source.MVVM.Model
{
    internal class UserModel : ModelBase
    {

        #region Construction

        /// <summary>
        /// Global unique identificator
        /// </summary>
        public Guid UID { get; }


        /// <summary>
        /// Name property of the user
        /// </summary>
        private string name;
        public string Name { get => name; }


        /// <summary>
        /// Bio property of the user
        /// </summary>
        private string bio;
        public string Bio { get => bio; }


        /// <summary>
        /// Constructor of User Model
        /// </summary>
        public UserModel(Guid uID, string name, string bio)
        {
            UID = uID;

            this.name = name;
            this.bio = bio;
        }

        #endregion
    }
}
