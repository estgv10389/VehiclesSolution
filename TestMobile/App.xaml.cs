using System.Text.Json;
using TestMobile.Models;
using TestMobile.Services;
using TestMobile.Views;

namespace TestMobile
{
    public partial class App : Application
    {
        public static List<Auction>? AuctionList { get; private set; }
        private readonly LoadingService _loadingService;
        public App()
        {
            InitializeComponent();
            _loadingService = new LoadingService();
            MainPage = new LoadingPage();
            LoadData();
        }

        private async void LoadData()
        {
            var loadingService = new LoadingService();
            AuctionList = await _loadingService.LoadingFile();
            MainPage = new AppShell();
        }
    }
}
