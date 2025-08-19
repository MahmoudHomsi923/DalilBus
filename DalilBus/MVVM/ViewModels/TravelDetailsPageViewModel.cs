using DalilBus.MVVM.Models;
using DalilBus.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DalilBus.MVVM.ViewModels
{
    public class TravelDetailsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly SharedDataService _sharedDataService;

        private bool isLoading = false;

        private int heightRequest = 200;

        public bool HasStopPlace1 => !string.IsNullOrEmpty(SelectedTravel?.StopPlaceNameEn1);

        public bool HasStopPlace2 => !string.IsNullOrEmpty(SelectedTravel?.StopPlaceNameEn2);

        public bool HasStopPlace3 => !string.IsNullOrEmpty(SelectedTravel?.StopPlaceNameEn3);

        public Travel? SelectedTravel
        {
            get => _sharedDataService.SelectedTravel;
            set
            {
                _sharedDataService.SelectedTravel = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public double TotalStopPlacesHeight
        {
            get
            {
                int visibleCount = 0;
                if (HasStopPlace1) visibleCount++;
                if (HasStopPlace2) visibleCount++;
                if (HasStopPlace3) visibleCount++;

                // 40px pro Haltestelle + 20px Abstand dazwischen
                return (visibleCount * 40) + ((visibleCount > 0 ? visibleCount - 1 : 0) * 20);
            }
        }

        public int HeightRequest => SelectedTravel == null ? heightRequest : heightRequest + (SelectedTravel.Changes * 50);

        public TravelDetailsPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
