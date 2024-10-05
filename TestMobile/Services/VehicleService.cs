using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Services
{
    public class VehicleService: BaseService
    {
       
        public VehicleService(HttpClient httpClient) : base(httpClient)
        {
           
        }


        public async Task<string> GetVehiclesAsync()
        {
            var response = await _httpClient.GetAsync("vehicles");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                throw new Exception("Erro ao buscar veículos.");
            }
        }
    }
}
