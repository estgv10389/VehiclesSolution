using TestMobile.ViewModels;

namespace TestMobile.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
    }
}