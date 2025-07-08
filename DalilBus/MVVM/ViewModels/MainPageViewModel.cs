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

        public MainPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
        }

        

        

        
    }
}
