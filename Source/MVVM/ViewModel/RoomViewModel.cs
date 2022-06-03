using Source.Core;
using Source.MVVM.Model;
using Source.Net;
using Source.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Source.MVVM.ViewModel
{
    internal class RoomViewModel : ViewModelBase
    {

        #region Construction 

        private readonly Server server;
        private readonly RoomModel room;


        /// <summary>
        /// Room's name property
        /// </summary>
        public string Name
        {
            get => room.Name;
        }


        public ObservableCollection<MessageModel> Messages { get; }


        /// <summary>
        /// Current user's message input
        /// </summary>
        private string currentMessage;
        public string CurrentMessage
        {
            get => currentMessage;
            set
            {
                currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }


        /// <summary>
        /// Goes back to home view
        /// </summary>
        public RelayCommand GoHomeCommand { get; }


        /// <summary>
        /// Sends message to a remote server based on current user message input
        /// </summary>
        public RelayCommand SendMessageCommand { get; }


        /// <summary>
        /// Constructor
        /// </summary>
        public RoomViewModel(ViewModelStore viewModelStore, Server server, RoomModel roomModel) : base(viewModelStore)
        {
            this.server = server;

            currentMessage = string.Empty;
            room = roomModel;

            GoHomeCommand = new RelayCommand(o => OnGoHome());
            SendMessageCommand = new RelayCommand(o => OnSendMessage(), w => currentMessage != string.Empty);

            Messages = new ObservableCollection<MessageModel>();
        }

        #endregion

        #region Server events handling

        private void OnMessageReceived(MessageModel message)
        {

        }

        private void OnMessageListReceived(List<MessageModel> messages)
        {
            messages.ForEach(x =>
            {
                if (x.Room.UID == room.UID && !new List<MessageModel>(Messages).Exists(y => y.UID == x.UID))
                {
                    for (int i = 0; i < Messages.Count; i++)
                    {
                        if (DateTime.Compare(Messages[i].Time, x.Time) < 0)
                        {
                            while (i < Messages.Count && DateTime.Compare(Messages[i].Time, x.Time) < 0) i++;

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Messages.Insert(i, x);
                            });
                            break;
                        }
                    }
                    if (Messages.Count == 0)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Messages.Add(x);
                        });
                    }
                }
            });
        }

        #endregion

        #region Commands handling

        private void OnGoHome()
        {
            ViewModelStore.CurrentViewModel = new HomeViewModel(ViewModelStore, server);
        }

        private void OnSendMessage()
        {
            server.SendMessagePacket(room.UID, currentMessage);
            CurrentMessage = string.Empty;
        }

        #endregion

        #region Method Overloading

        protected override void OnLoad()
        {
            server.MessageListReceived += OnMessageListReceived;

            server.SendMessageListRequest(room);
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            server.MessageListReceived -= OnMessageListReceived;

            base.OnUnload();
        }

        #endregion
    }
}
