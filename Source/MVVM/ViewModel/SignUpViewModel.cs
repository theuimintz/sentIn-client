using Source.Core;
using Source.Net;
using Source.Store;

namespace Source.MVVM.ViewModel
{
    internal class SignUpViewModel : ViewModelBase
    {
        #region Construction

        private readonly Server server;


        /// <summary>
        /// Username property
        /// </summary>
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        /// <summary>
        /// Password property
        /// </summary>
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        /// <summary>
        /// Processes Username and Password Input and sends in as a Sign In request
        /// </summary>
        public RelayCommand SubmitCommand { get; }


        /// <summary>
        /// Changes CurrentViewModel to LogInViewModel
        /// </summary>
        public RelayCommand SwitchViewCommand { get; }


        public SignUpViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            username = string.Empty;
            password = string.Empty;

            SubmitCommand = new RelayCommand(o => OnSubmit());
            SwitchViewCommand = new RelayCommand(o => OnGoToLogIn());
        }

        #endregion

        private void OnSubmit()
        {
            if (Username == string.Empty) return;
            else if (Password == string.Empty) return;

            server.SendSignUpRequest(Username, Password);
        }

        private void OnGoToLogIn()
        {
            ViewModelStore.CurrentViewModel = new SignInViewModel(ViewModelStore, server);
        }
    }
}
