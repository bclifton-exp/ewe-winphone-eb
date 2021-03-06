﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Devices.Geolocation;
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

        private Geopoint _mapCenter;
        public Geopoint MapCenter
        {
            get { return _mapCenter; }
            set
            {
                _mapCenter = value;
                OnPropertyChanged("MapCenter");
            }
        }

        private int _resultsCount;
        public int ResultsCount
        {
            get { return _resultsCount; }
            set
            {
                _resultsCount = value;
                HasResults = value > 0;
                OnPropertyChanged("ResultsCount");
            }
        }

        private bool _hasResults = true;
        public bool HasResults
        {
            get { return _hasResults; }
            set
            {
                _hasResults = value;
                OnPropertyChanged("HasResults");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
