using System.Text.Json;
using CurrencyTracker;
using Hangfire;

public class CurrencyService
{
  private readonly HttpClient _httpClient;
  private readonly ILogger<CurrencyService> _logger;

  public CurrencyService(HttpClient httpClient, ILogger<CurrencyService> logger)
  {
    _httpClient = httpClient;
    _logger = logger;
  }

  [AutomaticRetry(Attempts = 5, DelaysInSeconds = new[] { 60, 120, 300 })]
  public async Task FetchCurrencyRate()
  {
    var apiUrl = "https://api.exchangerate-api.com/v4/latest/USD";

    try
    {
      _logger.LogInformation($"Fetching currency rates at: {DateTime.Now}");

      var response = await _httpClient.GetAsync(apiUrl);
      response.EnsureSuccessStatusCode();

      var data = await response.Content.ReadAsStreamAsync();

      var currencyResponse = await JsonSerializer.DeserializeAsync<CurrencyResponse>(data, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});

      _logger.LogInformation($"Currency rates fetched at: {DateTime.Now}");
      _logger.LogInformation($"USD to UAH: {currencyResponse.Rates.UAH}");

    }
    catch (Exception ex)
    {
      _logger.LogError($"Error fetching currency data: {ex.Message}");
      throw; // Це дозволяє Hangfire повторити спробу виконання
    }
  }
}
