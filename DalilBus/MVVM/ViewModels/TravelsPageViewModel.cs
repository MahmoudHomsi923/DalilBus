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

        private const string RightArrowEmoji = " ➡️ "; // U+27A1 + U+FE0F
        private const string LeftArrowEmoji = " ⬅️ "; // U+2B05 + U+FE0F

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

        public string GetArrowEmoji() =>
            CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? LeftArrowEmoji : RightArrowEmoji;

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
