using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Entities;
using TrainLine.CurrencyExchange.Domain.Interfaces;
using TrainLine.CurrencyExchange.Infrastructure.Mappers;
using TrainLine.CurrencyExchange.Infrastructure.Proxies.Dtos;

namespace TrainLine.CurrencyExchange.Infrastructure.Proxies
{
    public class TrainlineExchangeRateProxy : IExchangeRateProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TrainlineExchangeRateProxy> _logger;
        private readonly IsoDateTimeConverter _dateConverter;

        // this should go in the config file, if any time left
        private const string baseUrl = @"https://trainlinerecruitment.github.io/exchangerates/";

        private readonly IEnumerable<TimeSpan> _retryPattern = new TimeSpan[2] {TimeSpan.FromMilliseconds(100),
                                TimeSpan.FromMilliseconds(200)};

        public TrainlineExchangeRateProxy(
            IHttpClientFactory httpClientFactory,
            ILogger<TrainlineExchangeRateProxy> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _dateConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(string currencyCode)
        {
            var response = await GetAsync<ExchangeRateDto>($"api/latest/{currencyCode}.json");
            return response?.ToDomain();
        }

        private HttpClient BuildHttpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var httpClient = BuildHttpClient();

                _logger.LogInformation($"GET {uri}");

                var response = await Policy
                           .HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                           .WaitAndRetryAsync(_retryPattern
                           , (result, timeSpan, retryCount, context) =>
                           {
                               _logger.LogError($"Request failed with {result.Result.StatusCode}. Retry count = {retryCount}. Waiting {timeSpan} before next retry. ");
                           })
                           .ExecuteAsync(() => httpClient.GetAsync(uri));

                _logger.LogInformation($"RESPONSE: {response}");

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogTrace($"CONTENT: {responseContent}");
                return JsonConvert.DeserializeObject<T>(responseContent, _dateConverter);
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAsync error:{e.Message}");
                return default(T);
            }
        }
    }
}