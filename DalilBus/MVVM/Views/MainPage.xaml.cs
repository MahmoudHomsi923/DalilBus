using DalilBus.Config;
using DalilBus.MVVM.ViewModels;
using Microsoft.Maui.Networking;

namespace DalilBus
{
    public partial class MainPage : ContentPage
    {
        private bool _isCheckingConnection = false;
        public MainPage()
        {
            InitializeComponent();
            // Set the title of the page
            Title = "Search بحث";
        }


        private async void OnBtnExchangeClicked(object sender, EventArgs e)
        {
            // Perform a 500ms rotation animation
            await btnExchange.RotateTo(180, 500);
            // Reset the rotation to its original state
            btnExchange.Rotation = 0;

            if (BindingContext is MainPageViewModel vm)
            {
                // Swap the start and destination points in the ViewModel
                vm.SwapPoints();
            }
        }

        private async void OnBtnSearchClicked(object sender, EventArgs e)
        {
            // Check for internet connectivity
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Show a message if there is no internet connection
                await DisplayAlert("لا يوجد اتصال بالإنترنت", "يرجى التحقق من اتصالك بالإنترنت والمحاولة مرة أخرى.\nPlease check your internet connection and try again.", "موافق / OK");
                return; // Exit the method if no connection is available
            }

            if (BindingContext is MainPageViewModel vm)
            {
                // Navigate to the TravelsPage with parameters
                await Shell.Current.GoToAsync($"TravelsPage?StartPlace={vm.SelectedStartPlace.Name}&DestinationPlace={vm.SelectedDestinationPlace.Name}&SelectedDate={vm.SelectedDate:yyyy-MM-dd}&SelectedTime={vm.SelectedTime:hh\\:mm}");
            }
        }

    }

}
