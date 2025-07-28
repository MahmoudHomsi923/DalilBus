using DalilBus.MVVM.Models;
using DalilBus.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DalilBus.MVVM.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly SharedDataService _sharedDataService;

        private ObservableCollection<Place>? _PlacesList;


        private bool isLoading = false;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place>? PlacesList
        {
            get { return _PlacesList; }
            set
            {
                _PlacesList = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace
        {
            get => _sharedDataService.SelectedStartPlace;
            set
            {
                if (_sharedDataService.SelectedStartPlace == value) return;
                _sharedDataService.SelectedStartPlace = value;
                OnPropertyChanged();
                if (_sharedDataService.SelectedStartPlace == SelectedDestinationPlace)
                    SelectedDestinationPlace = null;
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => _sharedDataService.SelectedDestinationPlace;
            set
            {
                if (_sharedDataService.SelectedDestinationPlace == value) return;
                _sharedDataService.SelectedDestinationPlace = value;
                OnPropertyChanged();
                if (_sharedDataService.SelectedDestinationPlace == SelectedStartPlace)
                    SelectedStartPlace = null;
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public DateTime SelectedDate
        {
            get => _sharedDataService.SelectedDate;
            set 
            {
                _sharedDataService.SelectedDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedTime
        {
            get => _sharedDataService.SelectedTime;
            set
            {
                _sharedDataService.SelectedTime = value;
                OnPropertyChanged();
            }
        }

        public bool CanSearch => SelectedStartPlace != null && SelectedDestinationPlace != null;

        public MainPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
        }

        public async Task InitializeOrReferechDataAsync()
        {
            if (PlacesList == null)
            {
                await _sharedDataService.LoadPlacesAsync();
                await _sharedDataService.LoadCompaniesAsync();
                PlacesList = new ObservableCollection<Place>(_sharedDataService.PlacesList);
                SelectedDate = DateTime.Now.Date; // Default to today's date
                SelectedTime = DateTime.Now.TimeOfDay; // Default to current time
            }
            else
            {
                SelectedDate = _sharedDataService.SelectedDate;
                SelectedTime = _sharedDataService.SelectedTime;
            }   
        }

        public async Task LoadTravelsAsync()
        {
            await _sharedDataService.LoadTravelsAsync();
        }

        public void SwapPoints()
        {
            var temp = SelectedStartPlace;
            SelectedStartPlace = SelectedDestinationPlace;
            SelectedDestinationPlace = temp;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
