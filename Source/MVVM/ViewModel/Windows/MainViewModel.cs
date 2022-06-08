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

        public MainViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            ViewModelStore.CurrentViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        protected override void OnLoad()
        {
            server.StartConnectingToServer();
            base.OnLoad();
        }

        #endregion
    }
}
