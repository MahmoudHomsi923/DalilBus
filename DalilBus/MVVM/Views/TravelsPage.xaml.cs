using DalilBus.MVVM.Models;
using DalilBus.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace DalilBus.MVVM.Views;

public partial class TravelsPage : ContentPage
{
    private readonly TravelsPageViewModel VM;

    public TravelsPage(TravelsPageViewModel travelsPageViewModel)
    {
        InitializeComponent();
        // Set the title here
        VM = travelsPageViewModel;
        // Initialize the BindingContext with TravelsPageViewModel instance
        BindingContext = VM ?? throw new ArgumentNullException(nameof(travelsPageViewModel), "TravelsPageViewModel cannot be null");
        // Set the title of the page
        Title = "Travels الرحلات";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

}
