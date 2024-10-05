using TestMobile.Services;
using System.Text.Json;
using TestMobile.Models;
using TestMobile.Pages;
namespace TestMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;

        private readonly HttpClient _httpClient;
        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _authService = new AuthService(_httpClient);
        }


        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Email and password are required", "OK");
                return;
            }

            try
            {
                string? jsonResponse = await _authService.LoginAsync(email, password);
                if (jsonResponse != null)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ApiResponse<object>? resp = JsonSerializer.Deserialize<ApiResponse<object>>(jsonResponse, options);
                    if (resp != null && resp.Success)
                    {
                        await SecureStorage.SetAsync("token", resp.Token);
                        Preferences.Set("auth", JsonSerializer.Serialize(resp.Data));
                        Application.Current.MainPage = new AppShell();
                        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                        return;
                    }
                }
                await DisplayAlert("Erro", "There's a problem with your request. Please try again later", "OK");
                return;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            SemanticScreenReader.Announce($"Entry completed with text: {((Entry)sender).Text}");
        }

        private void OnSignUpTapped(object sender, EventArgs e)
        {

        }
    }

}
