using Source.MVVM.View.Windows;
using Source.MVVM.ViewModel.Main;
using Source.MVVM.ViewModel.Windows;
using Source.Net;
using Source.Store;
using System.Windows;

namespace Source
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Server server = new Server();
            ViewModelStore store = new ViewModelStore();
            store.CurrentViewModel = new IntroViewModel(store, server);


            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(store, server)
            };
            mainWindow.Show();


            base.OnStartup(e);

        }
    }
}
