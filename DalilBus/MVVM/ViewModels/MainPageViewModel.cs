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

        private ObservableCollection<Place>? _availableStartPlacesList;
        private ObservableCollection<Place>? _availableDestinationPlacesList;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Place>? AvailableStartPlacesList
        {
            get { return _availableStartPlacesList; }
            set
            {
                _availableStartPlacesList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place>? AvailableDestinationPlacesList
        {
            get { return _availableDestinationPlacesList; }
            set
            {
                _availableDestinationPlacesList = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace
        {
            get => _selectedStartPlace;
            set
            {
                if (_selectedStartPlace == value) return;

                AvailableDestinationPlacesList = new ObservableCollection<Place>(
                    _sharedDataService.PlacesList.Where(p => value == null || p.Id != value.Id)
                );

                _selectedStartPlace = AvailableStartPlacesList?.Where(p => p.Id == value?.Id).FirstOrDefault() ?? null;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => _selectedDestinationPlace;
            set
            {
                if (_selectedDestinationPlace == value) return;

                AvailableStartPlacesList = new ObservableCollection<Place>(
                    _sharedDataService.PlacesList.Where(p => value == null || p.Id != value.Id)
                );

                _selectedDestinationPlace = AvailableDestinationPlacesList?.Where(p => p.Id == value?.Id).FirstOrDefault() ?? null;
                OnPropertyChanged();
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
             AvailableStartPlacesList = new ObservableCollection<Place>(_sharedDataService.PlacesList);
             AvailableDestinationPlacesList = new ObservableCollection<Place>(_sharedDataService.PlacesList);
        }

        private void UpdateStartList()
        {
            int tempId = SelectedDestinationPlace?.Id ?? 0;
            AvailableStartPlacesList = new ObservableCollection<Place>(
                _sharedDataService.PlacesList.Where(p => p.Id != tempId)
            );
        }

        private void UpdateDestinationList()
        {
            int tempId = SelectedStartPlace?.Id ?? 0;
            AvailableDestinationPlacesList = new ObservableCollection<Place>(
                _sharedDataService.PlacesList.Where(p => p.Id != tempId)
            );
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
