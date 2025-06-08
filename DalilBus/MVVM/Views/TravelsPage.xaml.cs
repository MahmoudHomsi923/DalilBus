using System.Data.SqlTypes;

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

    public TravelsPage()
    {
        InitializeComponent();
        // Set the title here
        Title = "Travels الرحلات";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Zeige die Werte auf der Seite an (z. B. in Labels)
        lblDateTime.Text = SelectedDate + "   " + SelectedTime;
        lblPlaces.Text = DestinationPlace + " <--- " + StartPlace;
    }
}
