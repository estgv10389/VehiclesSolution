using TestMobile.Models;
using TestMobile.ViewModels;

namespace TestMobile.Views.Auctions;

public partial class AuctionDetails : ContentPage
{
	public AuctionDetails(Auction selectedAuction)
	{
		InitializeComponent();
        BindingContext = new AuctionDetailPageViewModel(selectedAuction);
    }
}