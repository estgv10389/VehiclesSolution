using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestMobile.Services
{
    public class AuthService: BaseService
    {
        
        public AuthService(HttpClient httpClient): base(httpClient)
        {
        }

        public async Task<string> LoginAsync(string email, string password)
        {

            var loginData = new
            {
                Email = email,
                Password = password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("auth/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                throw new Exception("Erro ao fazer login.");
            }
        }
    }
}
