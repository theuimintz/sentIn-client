using System.Windows;
using System.Windows.Controls;

namespace Source.Components
{
    /// <summary>
    /// Interaction logic for InputField.xaml
    /// </summary>

    public partial class InputField : UserControl
    {

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(InputField), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(onPlaceholderPropertyChanged)));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(InputField), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(onTextPropertyChanged)));




        public InputField()
        {
            InitializeComponent();
        }

        private static void onPlaceholderPropertyChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            ((InputField)depObject).PlaceholderBlock.Text = (string)e.NewValue;
        }
        private static void onTextPropertyChanged(DependencyObject depObject, DependencyPropertyChangedEventArgs e)
        {
            if ((string)e.NewValue == ((InputField)depObject).FieldBox.Text) return;
            ((InputField)depObject).FieldBox.Text = (string)e.NewValue;
        }

        private void onFieldTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = FieldBox.Text;

            if (Text != string.Empty) PlaceholderBlock.Visibility = Visibility.Hidden;
            else PlaceholderBlock.Visibility = Visibility.Visible;
        }
    }

}
