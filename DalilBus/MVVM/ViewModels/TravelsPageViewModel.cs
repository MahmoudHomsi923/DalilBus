using DalilBus.MVVM.Models;
using DalilBus.Services;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace DalilBus.MVVM.ViewModels
{
    public class TravelsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly SharedDataService _sharedDataService;

        private Place? _selectedStartPlace;
        private Place? _selectedDestinationPlace;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;

        private const string RightArrowEmoji = " ➡️ "; // U+27A1 + U+FE0F
        private const string LeftArrowEmoji = " ⬅️ "; // U+2B05 + U+FE0F

        public Place? SelectedStartPlace => _selectedStartPlace;

        public Place? SelectedDestinationPlace => _selectedDestinationPlace;

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

        public string GetArrowEmoji() =>
            CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? LeftArrowEmoji : RightArrowEmoji;

        public TravelsPageViewModel(SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
            _selectedStartPlace = _sharedDataService.SelectedStartPlace;
            _selectedDestinationPlace = _sharedDataService.SelectedDestinationPlace;
            _selectedDate = _sharedDataService.SelectedDate;
            _selectedTime = _sharedDataService.SelectedTime;
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
