using DalilBus.MVVM.Models;
using DalilBus.MVVM.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace DalilBus.MVVM.Views;

public partial class TravelsPage : ContentPage
{
    private readonly TravelsPageViewModel vm;

    private bool datePickerInitialized = false;

    private bool timePickerInitialized = false;

    private bool hasDataLoaded = false;

    public TravelsPage(TravelsPageViewModel travelsPageViewModel)
    {
        InitializeComponent();
        // Set the title of the page
        Title = "Travels الرحلات";
        vm = travelsPageViewModel ?? throw new ArgumentNullException(nameof(travelsPageViewModel), "TravelsPageViewModel cannot be null");
        // Initialize the BindingContext with TravelsPageViewModel instance
        BindingContext = vm;  
    }

    protected override void OnAppearing()
    {
        if (hasDataLoaded)
        {
            hasDataLoaded = false;
            return;
        }

        base.OnAppearing();
        lblDirection.Text = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? (string)Resources["leftlong"] : (string)Resources["rightlong"];
        vm.IsLoading = true;
        vm.IntializeTravelsList();
        vm.IsLoading = false;
    }

    private async void OnDatePickerDateSelected(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DatePicker.Date))
        {
            if (!datePickerInitialized)
            {
                datePickerInitialized = true;
                return;
            }

            var picker = (DatePicker)sender;
            vm.SelectedDate = picker.Date;
            vm.IsLoading = true;
            await vm.LoadTravelsAsync();
            vm.IntializeTravelsList();
            vm.IsLoading = false;
        }
    }

    private async void OnTimePickerTimeSelected(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            if (!timePickerInitialized)
            {
                timePickerInitialized = true;
                return;
            }

            var picker = (TimePicker)sender;
            vm.SelectedTime = picker.Time;
            vm.IsLoading = true;
            await vm.LoadTravelsAsync();
            vm.IntializeTravelsList();
            vm.IsLoading = false;
        }
    }

    private async void OnTravelSelectedChangedAsync(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var selectedTravel = e.CurrentSelection[0] as Travel;
            if (selectedTravel != null)
            {
                await Shell.Current.GoToAsync("TravelDetailsPage");
            }
        }
        ((CollectionView)sender).SelectedItem = null;
        hasDataLoaded = true;
    }

}
