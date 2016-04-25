using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Expedia.Annotations;
using Expedia.Entities.Hotels;

namespace Expedia.Entities.Entities
{
    public class MapPushPin : MapElement, INotifyPropertyChanged
    {
        //public bool IsFlyoutVisible { get; set; }
        private bool _isFlyoutVisible;

        public bool IsFlyoutVisible
        {
            get { return _isFlyoutVisible; }
            set
            {
                _isFlyoutVisible = value;
                OnPropertyChanged("IsFlyoutVisible");
            }
        }

        public Geopoint Location { get; set; }
        public string Title { get; set; }
        public HotelResultItem HotelResultItem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
