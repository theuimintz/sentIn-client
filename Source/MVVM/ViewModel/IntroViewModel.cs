using Source.Core;
using Source.Net;
using Source.Store;

namespace Source.MVVM.ViewModel
{
    internal class IntroViewModel : ViewModelBase
    {
        private Server server;

        public RelayCommand SignInCommand { get; set; }
        public RelayCommand LogInCommand { get; set; }

        public IntroViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            SignInCommand = new RelayCommand(o => OnSignIn());
            LogInCommand = new RelayCommand(o => OnLogIn());
        }

        private void OnSignIn()
        {
            ViewModelStore.CurrentViewModel = new SignUpViewModel(ViewModelStore, server);
        }

        private void OnLogIn()
        {
            ViewModelStore.CurrentViewModel = new SignInViewModel(ViewModelStore, server);
        }
    }
}
