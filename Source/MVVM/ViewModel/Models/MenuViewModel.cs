using Source.Core;
using Source.Net;
using Source.Properties;
using System;
using System.ComponentModel;

namespace Source.MVVM.ViewModel.Models
{
    internal class MenuViewModel : INotifyPropertyChanged
    {

        private Server server;

        private Guid uid;
        public Guid UID
        {
            get => uid;
            set
            {
                uid = value;
                OnPropertyChanged(nameof(UID));
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }


        public RelayCommand? CreateRoomCommand { get; set; }

        public MenuViewModel(Server server)
        {
            this.server = server;
            this.isVisible = false;

            uid = Settings.Default.UID;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
