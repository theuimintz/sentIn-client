using System;
using System.Windows.Controls;

namespace Source.MVVM.View
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();
        }

        private void MessageListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer scrollViewer &&
                    Math.Abs(e.ExtentHeightChange) > 0.0)
            {
                scrollViewer.ScrollToBottom();
            }
        }

        private void OnMessageFieldContainerLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((TextBox)MessageField.FindName("FieldBox"))?.Focus();
        }
    }
}
