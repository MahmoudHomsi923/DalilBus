using DalilBus.MVVM.ViewModels;
using System.ComponentModel;
using System.Globalization;

namespace DalilBus.MVVM.Views;

public partial class TravelsPage : ContentPage
{
    private readonly TravelsPageViewModel vm;

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
            var picker = (DatePicker)sender;

            if ( vm.SelectedDate != picker.Date)
            {
                vm.SelectedDate = picker.Date;
                vm.IsLoading = true;
                await vm.LoadTravelsAsync();
                vm.IntializeTravelsList();
                vm.IsLoading = false;
            }
        }        
    }

    private async void OnTimePickerTimeSelected(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
        {
            var picker = (TimePicker)sender;

            if (vm.SelectedTime != picker.Time)
            {
                vm.SelectedTime = picker.Time;
                vm.IsLoading = true;
                await vm.LoadTravelsAsync();
                vm.IntializeTravelsList();
                vm.IsLoading = false;
            }
        }  
    }

}
