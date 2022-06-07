using Source.Components;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Source.MVVM.View
{
    /// <summary>
    /// Interaction logic for SigninView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void OnSubmit(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PasswordField.Password == string.Empty) (PasswordPanel.FindResource("ToError") as Storyboard)?.Begin();
            if (UsernameField.Text == string.Empty) (UsernamePanel.FindResource("ToError") as Storyboard)?.Begin();
        }
    }
}
