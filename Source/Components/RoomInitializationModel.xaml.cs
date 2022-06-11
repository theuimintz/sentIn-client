using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Source.Components
{
    public partial class RoomInitializationModel : UserControl
    {

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(RoomInitializationModel), new PropertyMetadata(false, new PropertyChangedCallback(OnVisibilityChanged)));


        public RoomInitializationModel()
        {
            InitializeComponent();

            if (IsVisible == false)
                Visibility = Visibility.Collapsed;
        }

        private static void OnVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            RoomInitializationModel obj = (RoomInitializationModel)o;
            obj.Visibility = Visibility.Visible;
            if ((bool)e.NewValue == false) obj.Visibility = Visibility.Collapsed;
            else ((Storyboard)obj.FindResource("In")).Begin();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            ((Storyboard)FindResource("Out")).Begin();
        }

        private void OnOutCompleted(object sender, System.EventArgs e)
        {
            IsVisible = false;
            GroupNameField.Text = "";
        }
    }
}
