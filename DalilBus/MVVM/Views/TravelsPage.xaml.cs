

using System.Globalization;

namespace DalilBus.MVVM.Views;

[QueryProperty(nameof(StartPlace), "StartPlace")]
[QueryProperty(nameof(DestinationPlace), "DestinationPlace")]
[QueryProperty(nameof(SelectedDate), "SelectedDate")]
[QueryProperty(nameof(SelectedTime), "SelectedTime")]
public partial class TravelsPage : ContentPage
{
    private string startPlace { get; set; } = string.Empty; // Initialize to avoid null
    private string destinationPlace { get; set; } = string.Empty; // Initialize to avoid null
    private string selectedDate { get; set; } = string.Empty; // Initialize to avoid null
    private string selectedTime { get; set; } = string.Empty; // Initialize to avoid null

    private const string RightArrowEmoji = " ➡️ "; // U+27A1 + U+FE0F

    private const string LeftArrowEmoji = " ⬅️ "; // U+2B05 + U+FE0F

    public string StartPlace
    {
        get => startPlace;
        set
        {
            startPlace = value;
            OnPropertyChanged();
        }
    }

    public string DestinationPlace
    {
        get => destinationPlace;
        set
        {
            destinationPlace = value;
            OnPropertyChanged();
        }
    }

    public string SelectedDate
    {
        get => selectedDate;
        set
        {
            selectedDate = value;
            OnPropertyChanged();
        }
    }

    public string SelectedTime
    {
        get => selectedTime;
        set
        {
            selectedTime = value;
            OnPropertyChanged();
        }
    }

    private string GetArrowEmoji() => 
        CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? LeftArrowEmoji : RightArrowEmoji;

    public TravelsPage()
    {
        InitializeComponent();
        // Set the title here
        Title = "Travels الرحلات";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblPlaces.Text = $"{StartPlace}   {GetArrowEmoji()}   {DestinationPlace}";
        lblDateTime.Text = $"{SelectedDate}  •  {SelectedTime}";
    }
}
