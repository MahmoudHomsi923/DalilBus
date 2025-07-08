using DalilBus.Config;
using DalilBus.Helper;
using DalilBus.MVVM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace DalilBus.Services
{
    public class SharedDataService
    {
        private List<Place> _placesList = new List<Place>();
        private List<Company> _companiesList = new List<Company>();
        private List<Travel> _travelsList = new List<Travel>();
        private Place? _selectedStartPlace;
        private Place? _selectedDestinationPlace;
        private DateTime selectedDate;
        private TimeSpan selectedTime;
        public bool CanSearch => SelectedStartPlace != null && SelectedDestinationPlace != null;
        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Place> PlacesList
        {
            get => _placesList;
            set
            {
                _placesList = value;
                OnPropertyChanged();
            }
        }

        public List<Company> CompaniesList
        {
            get => _companiesList;
            set
            {
                _companiesList = value;
                OnPropertyChanged();
            }
        }

        public List<Travel> TravelsList
        {
            get => _travelsList;
            set
            {
                _travelsList = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace
        {
            get => _selectedStartPlace;
            set
            {
                _selectedStartPlace = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => _selectedDestinationPlace;
            set
            {
                _selectedDestinationPlace = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set
            {
                selectedTime = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadPlacesAsync()
        {
            PlacesList = await ApiClient.GetFromSuberbaseAsync<List<Place>>(ApiConfig.PlacesEndpoint) ?? new List<Place>();
        }

        public async Task LoadCompaniesAsync()
        {
            CompaniesList = await ApiClient.GetFromSuberbaseAsync<List<Company>>(ApiConfig.CompaniesEndpoint) ?? new List<Company>();
        }

        public async Task LoadTravelsAsync()
        {
            TravelsList = await ApiClient.GetFromSuberbaseAsync<List<Travel>>(ApiConfig.TravelsEndpoint) ?? new List<Travel>();
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
