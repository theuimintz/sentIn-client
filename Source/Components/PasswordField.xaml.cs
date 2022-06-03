using System.Windows;
using System.Windows.Controls;

namespace Source.Components
{
    /// <summary>
    /// Interaction logic for PasswordField.xaml
    /// </summary>

    public partial class PasswordField : UserControl
    {

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(PasswordField), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(onPlaceholderPropertyChanged)));


        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordField), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(onPasswordPropertyChanged)));


        public PasswordField()
        {
            InitializeComponent();
        }

        public static void onPlaceholderPropertyChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordField)dependency).PlaceholderBlock.Text = (string)e.NewValue;
        }
        public static void onPasswordPropertyChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
        {
            if ((string)e.NewValue == ((PasswordField)dependency).FieldBox.Password) return;
            ((PasswordField)dependency).FieldBox.Password = (string)e.NewValue;
        }
        private void onPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = FieldBox.Password;

            if (Password != string.Empty) PlaceholderBlock.Visibility = Visibility.Hidden;
            else PlaceholderBlock.Visibility = Visibility.Visible;
        }
    }
}
