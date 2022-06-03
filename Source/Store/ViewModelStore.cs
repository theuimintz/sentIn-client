using Source.MVVM.ViewModel;
using System;
using System.Collections.Generic;

namespace Source.Store
{
    /// <summary>
    /// Stores CurrentViewModel
    /// </summary>
    internal class ViewModelStore
    {

        private List<ViewModelBase> viewModelBuffer; // To not recreate already loaded viewmodels

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action? CurrentViewModelChanged;

        public ViewModelStore()
        {
            viewModelBuffer = new List<ViewModelBase>();
            currentViewModel = new ViewModelBase(this);
        }
    }
}
