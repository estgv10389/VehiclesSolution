using TestMobile.Pages;

namespace TestMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnSignOutClicked(object sender, EventArgs e)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Sair", "Deseja realmente sair?", "Sim", "Não");
            if (!confirm)
                return;

            // Limpar estado de autenticação
            // await AuthService.LogoutAsync();
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}
