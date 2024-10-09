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
           
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await LoadData();  
        }

        private async Task LoadData()
        {
            var loadingService = new LoadingService();
            var filePath = "vehicles_dataset.json";
            AuctionList = await _loadingService.LoadingFile(filePath);
            MainPage = new AppShell();
        }
    }
}
