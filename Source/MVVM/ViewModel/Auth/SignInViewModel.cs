using Source.Core;
using Source.MVVM.ViewModel.Main;
using Source.Net;
using Source.Store;

namespace Source.MVVM.ViewModel.Auth
{
    internal class SignInViewModel : ViewModelBase
    {

        #region Construction

        private readonly Server server;


        /// <summary>
        /// Username Property
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
        /// Gets LoginCommand object
        /// </summary>
        public RelayCommand SubmitCommand { get; }


        /// <summary>
        /// Changes CurrentViewModel to the SignInViewModel
        /// </summary>
        public RelayCommand SwitchViewCommand { get; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public SignInViewModel(ViewModelStore viewModelStore, Server server) : base(viewModelStore)
        {
            this.server = server;

            this.server.LoggedIn += OnLoggedIn;
            this.server.LogInRejected += OnLogInRejected;


            username = "";
            password = "";

            SubmitCommand = new RelayCommand(o => OnSubmit());
            SwitchViewCommand = new RelayCommand(o => OnSwitchView());
        }

        #endregion

        #region Server event handling

        /// <summary>
        /// Calls when server accepts login data
        /// </summary>
        private void OnLoggedIn()
        {
            ViewModelStore.CurrentViewModel = new HomeViewModel(ViewModelStore, server);
        }


        /// <summary>
        /// Calls when server rejects login data
        /// </summary>
        private void OnLogInRejected()
        {

        }

        #endregion

        #region Commands handling 

        /// <summary>
        /// Switches current ViewModel to SignInViewModel
        /// </summary>
        private void OnSwitchView()
        {
            ViewModelStore.CurrentViewModel = new SignUpViewModel(ViewModelStore, server);
        }


        /// <summary>
        /// Validates input data and processes it
        /// </summary>
        private void OnSubmit()
        {
            if (Username == string.Empty) return;
            else if (Password == string.Empty) return;

            server.SendSignInRequest(Username, Password);
        }

        #endregion

        #region Method overrides

        protected override void OnUnload()
        {
            server.LoggedIn -= OnLoggedIn;
            server.LogInRejected -= OnLogInRejected;

            base.OnUnload();
        }

        #endregion

    }
}
