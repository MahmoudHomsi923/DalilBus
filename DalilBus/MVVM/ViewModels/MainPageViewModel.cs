using DalilBus.MVVM.Models;
using DalilBus.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DalilBus.MVVM.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly SharedDataService _sharedDataService;
        private ObservableCollection<Place>? _availableStartPlaces;
        private ObservableCollection<Place>? _availableDestinationPlaces;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Place>? AvailableStartPlaces
        {
            get { return _availableStartPlaces; }
            set
            {
                _availableStartPlaces = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place>? AvailableDestinationPlaces
        {
            get { return _availableDestinationPlaces; }
            set
            {
                _availableDestinationPlaces = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace
        {
            get => _sharedDataService.SelectedStartPlace;
            set 
            {
                _sharedDataService.SelectedStartPlace = value;
                AvailableDestinationPlaces = new ObservableCollection<Place>(_sharedDataService.PlacesList.Where(p => _sharedDataService.SelectedStartPlace == null ||
                                    p.Id != _sharedDataService.SelectedStartPlace.Id).ToList());
                OnPropertyChanged(nameof(AvailableDestinationPlaces));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => _sharedDataService.SelectedDestinationPlace;
            set
            {
                _sharedDataService.SelectedDestinationPlace = value;
                AvailableStartPlaces = new ObservableCollection<Place>(_sharedDataService.PlacesList.Where(p => _sharedDataService.SelectedDestinationPlace == null ||
                                    p.Id != _sharedDataService.SelectedDestinationPlace.Id).ToList());
                OnPropertyChanged(nameof(AvailableStartPlaces));
            }
        }

        public DateTime SelectedDate
        {
            get => _sharedDataService.SelectedDate; set => _sharedDataService.SelectedDate = value;
        }

        public TimeSpan SelectedTime
        {
            get => _sharedDataService.SelectedTime; set => _sharedDataService.SelectedTime = value;
        }

        public bool CanSearch => _sharedDataService.CanSearch;

        public MainPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
        }

        public async Task InitializeDataAsync()
        {
            AvailableStartPlaces = new ObservableCollection<Place>(_sharedDataService.PlacesList);
            AvailableDestinationPlaces = new ObservableCollection<Place>(_sharedDataService.PlacesList);
        }

        public async Task LoadPlacesAsync()
        {
            await _sharedDataService.LoadPlacesAsync();
        }

        public async Task LoadCompaniesAsync()
        {
            await _sharedDataService.LoadCompaniesAsync();
        }

        public async Task LoadTravelsAsync()
        {
            await _sharedDataService.LoadTravelsAsync();
        }
        public void SwapPoints()
        {
            _sharedDataService.SwapPoints();
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
