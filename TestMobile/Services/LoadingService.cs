using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestMobile.Models;

namespace TestMobile.Services
{
    public class LoadingService
    {
         public async Task<List<Auction>> LoadingFile()
        {
            List<Auction> ListAuction = new List<Auction>();
            var filePath = "vehicles.json";

            try
            {
                using Stream stream = await FileSystem.OpenAppPackageFileAsync(filePath);
                using StreamReader reader = new StreamReader(stream);
                string jsonString = await reader.ReadToEndAsync();

                ListAuction = JsonSerializer.Deserialize<List<Auction>>(jsonString) ?? new List<Auction>();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
                ListAuction = new List<Auction>();
            }

            return ListAuction;
        }
    }
}
