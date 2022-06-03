using Source.Core;
using Source.Store;
using System.ComponentModel;

namespace Source.MVVM.ViewModel
{
    /// <summary>
    /// Base class for ViewModels
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged
    {
        #region Construction

        protected ViewModelStore ViewModelStore;

        public RelayCommand LoadCommand { get; }
        public RelayCommand UnloadCommand { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="viewModelStore"></param>
        public ViewModelBase(ViewModelStore viewModelStore)
        {
            ViewModelStore = viewModelStore;

            LoadCommand = new RelayCommand(o => OnLoad());
            UnloadCommand = new RelayCommand(o => OnUnload());
        }

        /// <summary>
        /// Should be called once view is loaded
        /// </summary>
        protected virtual void OnLoad()
        {
            // Do some stuff
        }

        /// <summary>
        /// Should be called once view is unloaded
        /// </summary>
        protected virtual void OnUnload()
        {
            // Do some stuff
        }

        #endregion

        #region INotifyPropertyChanged interface realization

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
