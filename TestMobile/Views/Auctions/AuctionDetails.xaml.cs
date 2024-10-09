using TestMobile.Models;
using TestMobile.Services;
using TestMobile.ViewModels;

namespace TestMobile.Views.Auctions;

public partial class AuctionDetails : ContentPage
{
    private AuctionDetailPageViewModel _viewModel;

    public AuctionDetails(Auction selectedAuction)
	{
		InitializeComponent();
        var dialogService = new DialogService(this);
        _viewModel = new AuctionDetailPageViewModel(selectedAuction, dialogService, Navigation);
        BindingContext = _viewModel;
    }
}