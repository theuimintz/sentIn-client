using Source.Core;
using Source.Net;
using System.ComponentModel;

namespace Source.MVVM.ViewModel.Models
{
    internal class RoomInitializationViewModel : INotifyPropertyChanged
    {
        private Server server;

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(isVisible));
            }
        }

        private string roomName;
        public string RoomName
        {
            get => roomName;
            set
            {
                roomName = value;
                OnPropertyChanged(RoomName);
            }
        }

        public RelayCommand CreateRoomCommand { get; }

        public RoomInitializationViewModel(Server server)
        {
            this.server = server;
            this.isVisible = false;

            this.roomName = "";

            CreateRoomCommand = new RelayCommand(o => OnCreateRoom());
        }

        private void OnCreateRoom()
        {
            if (roomName != string.Empty)
            {
                server.SendRoomCreationRequest(roomName);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
