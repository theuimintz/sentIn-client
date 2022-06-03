using Source.MVVM.ViewModel;
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
    internal class HomeViewModel : ViewModelBase
    {

        #region Construction

        private readonly Server server;

        /// <summary>
        /// Room collection of current session
        /// </summary>
        public ObservableCollection<RoomModel> RoomList { get; }

        public RelayCommand EnterRoomCommand { get; }

        /// <summary>
        /// Constructor of Home ViewModel
        /// </summary>
        public HomeViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            RoomList = new ObservableCollection<RoomModel>();
            EnterRoomCommand = new RelayCommand(o => OnEnterRoom((RoomModel)o));
        }

        #endregion

        #region Server events handling

        /// <summary>
        /// Calls when room collection was received from server
        /// </summary>
        private void OnRoomListReceived(List<RoomModel> rooms)
        {
            rooms.ForEach(room =>
            {
                if (!new List<RoomModel>(RoomList).Exists(x => x.UID == room.UID))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        RoomList.Add(room);
                    });
                }
            });
        }


        /// <summary>
        /// Calls when message collection was received from server
        /// </summary>
        private void OnMessageListReceived(List<MessageModel> messages)
        {
            messages.ForEach(x =>
            {
                RoomModel? room = new List<RoomModel>(RoomList).Find(y => y.UID == x.Room.UID);

                if (room != null && room.LastMessage != null && DateTime.Compare(room.LastMessage.Time, x.Time) < 0 || room != null && room.LastMessage == null)
                {
                    room.LastMessage = x;
                }
            });
        }

        #endregion

        #region Commands handling

        /// <summary>
        /// Changes current viewmodel to the given room
        /// </summary>
        private void OnEnterRoom(RoomModel room)
        {
            ViewModelStore.CurrentViewModel = new RoomViewModel(ViewModelStore, server, room);
        }

        #endregion

        #region Base class override

        protected override void OnLoad()
        {
            server.RoomListReceived += OnRoomListReceived;
            server.MessageListReceived += OnMessageListReceived;

            server.SendRoomListRequest();
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            server.RoomListReceived -= OnRoomListReceived;
            server.MessageListReceived -= OnMessageListReceived;

            base.OnUnload();
        }

        #endregion

    }
}
