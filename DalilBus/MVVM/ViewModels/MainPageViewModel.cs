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

        private Place? _selectedStartPlace;
        private Place? _selectedDestinationPlace;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;

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
            get => _selectedStartPlace;
            set
            {
                if (_selectedStartPlace == value) return;
                _selectedStartPlace = value;
                OnPropertyChanged();
                if (_selectedStartPlace == SelectedDestinationPlace)
                    SelectedDestinationPlace = null;
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => _selectedDestinationPlace;
            set
            {
                if (_selectedDestinationPlace == value) return;
                _selectedDestinationPlace = value;
                OnPropertyChanged();
                if (_selectedDestinationPlace == SelectedStartPlace)
                    SelectedStartPlace = null;
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set 
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                _selectedTime = value;
                OnPropertyChanged();
            }
        }

        public bool CanSearch => SelectedStartPlace != null && SelectedDestinationPlace != null;

        public MainPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
            SelectedDate = DateTime.Now.Date; // Default to today's date
            SelectedTime = DateTime.Now.TimeOfDay; // Default to current time
        }

        public void InitializeAvailableLists()
        {
            PlacesList = new ObservableCollection<Place>(_sharedDataService.PlacesList);
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

        public void InitializeSharedDataService()
        {
            _sharedDataService.SelectedStartPlace = SelectedStartPlace;
            _sharedDataService.SelectedDestinationPlace = SelectedDestinationPlace;
            _sharedDataService.SelectedDate = SelectedDate;
            _sharedDataService.SelectedTime = SelectedTime;
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
