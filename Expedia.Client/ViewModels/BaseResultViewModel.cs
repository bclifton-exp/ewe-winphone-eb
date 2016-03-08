using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml.Controls.Maps;

namespace Expedia.Client.ViewModels
{
    public abstract class BaseResultViewModel : INotifyPropertyChanged
    {
        private MapControl _mapControl;
        public MapControl MapControl
        {
            get { return _mapControl; }
            set
            {
                _mapControl = value;
                OnPropertyChanged("MapControl");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
