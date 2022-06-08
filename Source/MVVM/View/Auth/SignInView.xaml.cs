using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Source.MVVM.View.Auth
{
    /// <summary>
    /// Interaction logic for AuthView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
        }

        private void OnSubmit(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PasswordField.Password == string.Empty) (PasswordPanel.FindResource("ToError") as Storyboard)?.Begin();
            if (UserField.Text == string.Empty) (UsernamePanel.FindResource("ToError") as Storyboard)?.Begin();
        }
    }
}
