using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CurrencyTrackerFunction
{
  public class FetchCurrencyRate
  {
    private readonly ILogger _logger;
    private readonly ICurrencyService _currencyService;

    public FetchCurrencyRate(ILoggerFactory loggerFactory, ICurrencyService currencyService)
    {
      _currencyService = currencyService;
      _logger = loggerFactory.CreateLogger<FetchCurrencyRate>();
    }

    [Function("FetchCurrencyRate")]
    //[ExponentialBackoffRetry(5, "00:00:10", "00:05:00")]
    [FixedDelayRetry(3, "00:00:30")]
    public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
    {
      _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
      var currencyResponse = await _currencyService.FetchCurrencyRate();

      if (myTimer.ScheduleStatus is not null)
      {
        _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
      }
      _logger.LogInformation($"Currency rates fetched at: {DateTime.Now}");
      _logger.LogInformation($"USD to UAH: {currencyResponse.Rates.UAH}");
    }
  }
}
