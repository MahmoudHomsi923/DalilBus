using DalilBus.MVVM.Models;
using DalilBus.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DalilBus.MVVM.ViewModels
{
    public class TravelsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly SharedDataService _sharedDataService;

        private bool isLoading = false;

        private ObservableCollection<Travel>? travelsList;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Travel>? TravelsList
        {
            get => travelsList;
            set
            {
                travelsList = value;
                OnPropertyChanged();
            }
        }

        public Place? SelectedStartPlace => _sharedDataService.SelectedStartPlace;

        public Place? SelectedDestinationPlace => _sharedDataService.SelectedDestinationPlace;

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

        public async Task LoadTravelsAsync()
        {
            await _sharedDataService.LoadTravelsAsync();
        }

        public void IntializeTravelsList()
        {
            TravelsList = new ObservableCollection<Travel>(_sharedDataService.TravelsList);
        }

        public string GetCompanyNamebyID(int companyId)
        {
            var company = _sharedDataService.CompaniesList.FirstOrDefault(c => c.Id == companyId);
            return company?.Name ?? "Unknown Company";
        }

        public TravelsPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
