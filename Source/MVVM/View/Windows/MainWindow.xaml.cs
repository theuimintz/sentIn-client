using System.Windows;

namespace Source.MVVM.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event method handlers

        /// <summary>
        /// Calls, whenever user clicks the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Calls, once user downed his mouse left button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockPanelMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }

        #endregion
    }
}
