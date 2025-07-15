using DalilBus.Config;
using DalilBus.MVVM.ViewModels;


namespace DalilBus
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _mainPageViewModel;

        public MainPage(MainPageViewModel mainPageViewModel )
        {
            InitializeComponent();
            // Set the title of the page
            Title = "Search بحث";
            // Intialize MainPageViewModel 
            _mainPageViewModel = mainPageViewModel ?? throw new ArgumentNullException(nameof(mainPageViewModel), "MainPageViewModel cannot be null");
            BindingContext = _mainPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(500);

            while (!ConnectivityHelper.IsConnectedToInternet())
            {
                await DisplayAlert(
            "لا يوجد اتصال بالإنترنت",
            "يرجى التحقق من اتصالك بالإنترنت والمحاولة مرة أخرى.\nPlease check your internet connection and try again.",
            "موافق / OK");

                await Task.Delay(1000);
            }


            if (_mainPageViewModel != null)
            {
                _mainPageViewModel.IsLoading = true;

                await Task.WhenAll(
                    _mainPageViewModel.LoadTravelsAsync(),
                    _mainPageViewModel.LoadPlacesAsync(),
                    _mainPageViewModel.LoadCompaniesAsync()
                );
                _mainPageViewModel.InitializeAvailableLists();
                _mainPageViewModel.IsLoading = false;
            }
        }

        private async void OnBtnExchangeClicked(object sender, EventArgs e)
        {
            // Perform a 500ms rotation animation
            await btnExchange.RotateTo(180, 500);
            // Reset the rotation to its original state
            btnExchange.Rotation = 0;

            // Swap the start and destination points in the ViewModel
            _mainPageViewModel.SwapPoints();
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

            if (_mainPageViewModel != null)
            {
                _mainPageViewModel.InitializeSharedDataService();
                // Navigate to the TravelsPage with parameters
                await Shell.Current.GoToAsync("TravelsPage");
            }
        }
    }

}
