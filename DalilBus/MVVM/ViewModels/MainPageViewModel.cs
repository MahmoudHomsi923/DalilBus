using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DalilBus.Config;
using DalilBus.MVVM.Models;
using Microsoft.Maui.Networking;

namespace DalilBus.MVVM.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Place>? places = null;
        private Place? selectedStartPlace = null;
        private Place? selectedDestinationPlace = null;
        private DateTime selectedDate;
        private TimeSpan selectedTime;
        public bool CanSearch => SelectedStartPlace != null && SelectedDestinationPlace != null;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Place>? Places
        {
            get => places;
            set
            {
                places = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace
        {
            get => selectedStartPlace;
            set
            {
                selectedStartPlace = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSearch));
            }
        }

        public Place? SelectedDestinationPlace
        {
            get => selectedDestinationPlace;
            set
            {
                selectedDestinationPlace = value;
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

        public MainPageViewModel()
        {
            Places = new ObservableCollection<Place>();
            SelectedDate = DateTime.Today;
            SelectedTime = DateTime.Now.TimeOfDay;

            if (ConnectivityHelper.IsConnectedToInternet())
            {
                // Punkte aus der API laden  
                _ = LoadPointsAsync();
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

        private async Task LoadPointsAsync()
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
                throw new Exception("Fehler beim Laden der Punkte: " + ex.Message);
            }
        }
    }
}
