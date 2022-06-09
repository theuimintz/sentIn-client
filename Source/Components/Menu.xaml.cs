using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Source.Components
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(Menu), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnControlVisibilityChanged)));


        public Menu()
        {
            InitializeComponent();
        }

        private static void OnControlVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Menu obj = (Menu)o;
            obj.Visibility = Visibility.Visible;

            if ((bool)e.NewValue == true)
                ((Storyboard)obj.FindResource("In")).Begin();
            else ((Storyboard)obj.FindResource("Out")).Begin();
        }

        private void OnRectangleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsVisible = false;
        }

        private void Out_Completed(object sender, System.EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
