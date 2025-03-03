using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CurrencyTrackerFunction;

public interface ICurrencyService
{
  Task<CurrencyResponse> FetchCurrencyRate();
}

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CurrencyService> _logger;

    public CurrencyService(HttpClient httpClient, ILogger<CurrencyService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CurrencyResponse> FetchCurrencyRate()
    {
        var apiUrl = "https://api.exchangerate-api.com/v4/latest/USD";

        try
        {
            _logger.LogInformation($"Fetching currency rates at: {DateTime.Now}");

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<CurrencyResponse>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching currency data: {ex.Message}");
            throw; // Це дозволяє Hangfire повторити спробу виконання
        }
    }
}