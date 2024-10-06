using System.Text.Json;
using TestMobile.Models;
using TestMobile.Services;

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
            LoadData();
            MainPage = new MainPage();
            
        }

        private async void LoadData()
        {
            var loadingService = new LoadingService();
            AuctionList = await _loadingService.LoadingFile();
        }
    }
}
