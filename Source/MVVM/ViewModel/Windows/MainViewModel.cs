using Source.MVVM.ViewModel.Models;
using Source.Net;
using Source.Store;

namespace Source.MVVM.ViewModel.Windows
{
    internal class MainViewModel : ViewModelBase
    {
        #region Construction

        /// <summary>
        /// Server class, that communicates with a remote server 
        /// </summary>
        private readonly Server server;

        /// <summary>
        /// CurrentViewModel of MainViewModel
        /// </summary>
        public ViewModelBase CurrentViewModel => ViewModelStore.CurrentViewModel;


        /// <summary>
        /// Respresents the view-model of the menu
        /// </summary>
        public MenuViewModel MenuViewModel { get; }


        /// <summary>
        /// Represents the view-model of room initialization model
        /// </summary>
        public RoomInitializationViewModel RoomInitializationViewModel { get; }


        public MainViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            MenuViewModel = new MenuViewModel(server);
            MenuViewModel.CreateRoomCommand = new Core.RelayCommand(o => OnCreateRoom());
            RoomInitializationViewModel = new RoomInitializationViewModel(server);

            ViewModelStore.CurrentViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCreateRoom()
        {
            MenuViewModel.IsVisible = false;
            RoomInitializationViewModel.IsVisible = true;
        }

        protected override void OnLoad()
        {
            server.StartConnectingToServer();
            base.OnLoad();
        }

        #endregion
    }
}
