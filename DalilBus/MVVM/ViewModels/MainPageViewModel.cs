using DalilBus.MVVM.Models;
using DalilBus.Services;
using System.Collections.ObjectModel;

namespace DalilBus.MVVM.ViewModels
{
    public class MainPageViewModel
    {
        private readonly SharedDataService _sharedDataService;

        public ObservableCollection<Place> AvailableStartPlaces =>
                    new(_sharedDataService.PlacesList.Where(p => _sharedDataService.SelectedDestinationPlace == null ||
                                    p.Id != _sharedDataService.SelectedDestinationPlace.Id).ToList());

        public ObservableCollection<Place> AvailableDestinationPlaces =>
            new(_sharedDataService.PlacesList.Where(p => _sharedDataService.SelectedStartPlace == null ||
                            p.Id != _sharedDataService.SelectedStartPlace.Id).ToList());

        public Place? SelectedStartPlace
        {
            get => _sharedDataService.SelectedStartPlace; set => _sharedDataService.SelectedStartPlace = value;
        }

        public Place? SelectedDestinationPlace
        {
            get => _sharedDataService.SelectedDestinationPlace; set => _sharedDataService.SelectedDestinationPlace = value;
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

    }
}
