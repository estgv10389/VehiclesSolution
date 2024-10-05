using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _httpClient;
        protected BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            string? baseUri = Environment.GetEnvironmentVariable("API_BASE_URI");
            if (string.IsNullOrEmpty(baseUri))
            {
                throw new Exception("A variável de ambiente 'API_BASE_URI' não está configurada.");
            }

            _httpClient.BaseAddress = new Uri(baseUri);
        }
    }
}
