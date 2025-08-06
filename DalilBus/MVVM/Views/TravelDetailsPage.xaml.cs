using DalilBus.MVVM.ViewModels;

namespace DalilBus.MVVM.Views;
public partial class TravelDetailsPage : ContentPage
{
    private readonly TravelDetailsPageViewModel vm;

    public TravelDetailsPage(TravelDetailsPageViewModel travelDetailsPageViewModel)
	{
		InitializeComponent();
        // Set the title of the page
        Title = "Travel Details تفاصيل الرحلة";
        vm = travelDetailsPageViewModel ?? throw new ArgumentNullException(nameof(travelDetailsPageViewModel), "TravelDetailsPageViewModel cannot be null");
        // Initialize the BindingContext with TravelDetailsPageViewModel instance
        BindingContext = vm;
    }
}