using System.Windows;
using System.Windows.Controls;

namespace Source.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void OnSearchFieldContainerLoaded(object sender, RoutedEventArgs e)
        {
            (SearchField.FindName("FieldBox") as TextBox)?.Focus();
        }
    }
}
