using System.Text;
using System.Text.Json;
using WebAppRazorSandwitchClient;

namespace WebAppRazorSandwitchClient
{
    public class SandwitchService
    {
        public SandwitchService()
        {

        }

        public async Task<List<SandwitchModel>> GetSandwiches()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7256/api/Sandwitch");

                var sandwiches = await JsonSerializer.DeserializeAsync<List<SandwitchModel>>(stream);
                return sandwiches ?? new List<SandwitchModel>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw;
            }
        }
        public async Task<SandwitchModel?> AddSandwitch(SandwitchModel sandwitch)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7256/api/Sandwitch", sandwitch);

                if (!response.IsSuccessStatusCode)
                {
                    // Optionally log or inspect the error response
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                var createsandwitch = await response.Content.ReadFromJsonAsync<SandwitchModel>();
                return createsandwitch;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<SandwitchModel?> GetSandwitchByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7256/api/Sandwitch/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var sandwich = await response.Content.ReadFromJsonAsync<SandwitchModel>();
                return sandwich;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<SandwitchModel?> UpdateSandwitchAsync(SandwitchModel sandwich)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7256/api/Sandwitch/{sandwich.Id}", sandwich);

                if (!response.IsSuccessStatusCode)
                    return null;

                var updatedSandwich = await response.Content.ReadFromJsonAsync<SandwitchModel>();
                return updatedSandwich;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteSandwitchAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7256/api/Sandwitch/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}