using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DalilBus.Config;
using DalilBus.MVVM.Models;

namespace DalilBus.MVVM.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Place> _places = new ObservableCollection<Place>();
        private ObservableCollection<Place> _availableStartPlaces = new ObservableCollection<Place>();
        private ObservableCollection<Place> _availableDestinationPlaces = new ObservableCollection<Place>();
        private Place? _selectedStartPlace;
        private Place? _selectedDestinationPlace;
        private DateTime selectedDate;
        private TimeSpan selectedTime;

        public bool CanSearch => SelectedStartPlace != null && SelectedDestinationPlace != null;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Place> Places
        {
            get => _places;
            set
            {
                _places = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place> AvailableStartPlaces
        {
            get
            {
                return new ObservableCollection<Place>(
                    Places.Where(p => SelectedDestinationPlace == null ||
                                    p.Id != SelectedDestinationPlace.Id).ToList());
            }
            set
            {
                _availableStartPlaces = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place> AvailableDestinationPlaces
        {
            get
            {
                return new ObservableCollection<Place>(
                    Places.Where(p => SelectedStartPlace == null ||
                                    p.Id != SelectedStartPlace.Id).ToList());
            }
            set
            {
                _availableDestinationPlaces = value;
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
                OnPropertyChanged(nameof(AvailableDestinationPlaces));
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
                OnPropertyChanged(nameof(AvailableStartPlaces));
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

        public MainPageViewModel()
        {
            // Initialize the selected places to null
            SelectedStartPlace = null;
            SelectedDestinationPlace = null;
            // Initialize the date and time to today and now
            SelectedDate = DateTime.Today;
            SelectedTime = DateTime.Now.TimeOfDay;
            // Load the places asynchronously
            InitializeData();
        }

        private async void InitializeData()
        {
            try
            {
                if (ConnectivityHelper.IsConnectedToInternet())
                {
                    await LoadPlacesAsync();
                }
                AvailableStartPlaces = new ObservableCollection<Place>(Places);
                AvailableDestinationPlaces = new ObservableCollection<Place>(Places);
            }
            catch (Exception ex)
            {
                throw new Exception($"Initialization failed: {ex.Message}");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SwapPoints()
        {
            var temp = SelectedStartPlace;
            SelectedStartPlace = SelectedDestinationPlace;
            SelectedDestinationPlace = temp;
        }

        public async Task LoadPlacesAsync()
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("apiKey", ApiConfig.ApiKey); // Falls erforderlich  
                var response = await client.GetStringAsync($"{ApiConfig.BaseUrl}{ApiConfig.PlacesEndpoint}");
                var _places = JsonSerializer.Deserialize<List<Place>>(response);

                if (_places != null) // Ensure points is not null
                {
                    Places.Clear();
                    foreach (var place in _places)
                    {
                        Places.Add(place);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load places: " + ex.Message);
            }
        }
    }
}
