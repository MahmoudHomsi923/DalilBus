using DalilBus.MVVM.Views;

namespace DalilBus
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.CurrentItem = this.FindByName<ShellContent>("searchTab");
            this.Navigated += OnNavigated;
            // Route für TravelsPage registrieren
            Routing.RegisterRoute("TravelsPage", typeof(TravelsPage));
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (Shell.Current.CurrentPage is Page currentPage)
            {
                TitleLabel.Text = currentPage.Title;
            }
        }
    }
}
