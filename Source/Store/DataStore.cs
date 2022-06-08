using System;

namespace Source.Store
{
    internal class DataStore<T>
    {
        private T? currentValue;
        public T? CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                DataChanged?.Invoke();
            }
        }

        public event Action? DataChanged;
    }
}
