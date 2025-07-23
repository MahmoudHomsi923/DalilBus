using DalilBus.Config;
using DalilBus.MVVM.Models;
using DalilBus.MVVM.ViewModels;
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        lblDirection.Text = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? (string)Resources["leftlong"] : (string)Resources["rightlong"];

        await Task.Delay(500);

        while (!ConnectivityHelper.IsConnectedToInternet())
        {
            await DisplayAlert(
        "لا يوجد اتصال بالإنترنت",
        "يرجى التحقق من اتصالك بالإنترنت والمحاولة مرة أخرى.\nPlease check your internet connection and try again.",
        "موافق / OK");

            await Task.Delay(1000);
        }

        if (vm != null)
        {
            vm.IsLoading = true;
            await vm.IntializeDataAsync();
            vm.IsLoading = false;
        }
    }

}
