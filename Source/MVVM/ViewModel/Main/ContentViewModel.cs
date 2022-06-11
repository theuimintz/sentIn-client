using Source.Net;
using Source.Store;

namespace Source.MVVM.ViewModel.Main
{
    internal class ContentViewModel : ViewModelBase
    {
        private ViewModelStore pageStore;
        private Server server;

        public ViewModelBase CurrentPage => pageStore.CurrentViewModel;

        public ContentViewModel(ViewModelStore viewModelStore, Server server, ViewModelBase startupPage) : base(viewModelStore)
        {
            pageStore = new ViewModelStore();
            pageStore.CurrentViewModel = startupPage;
            pageStore.CurrentViewModelChanged += OnPageChanged;

            this.server = server;
        }

        private void OnPageChanged()
        {
            OnPropertyChanged(nameof(CurrentPage));
        }

    }
}
