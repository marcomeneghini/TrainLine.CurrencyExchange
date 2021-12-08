using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Exceptions;
using TrainLine.CurrencyExchange.Domain.Interfaces;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class CurrencyFactory : ICurrencyFactory
    {
        private readonly IMemoryCache cache;
        private readonly IExchangeRateProxy exchangeRateProxy;

        public CurrencyFactory(
            IMemoryCache cache,
            IExchangeRateProxy exchangeRateProxy)
        {
            this.cache = cache;
            this.exchangeRateProxy = exchangeRateProxy;
        }

        /// <summary>
        /// Retrieve a Currency Entity
        /// </summary>
        /// <param name="currencyCode">Currency Code (USD/EUR/GBP)</param>
        /// <param name="validCacheSeconds">THIS PARAMENTER IS HERE BECAUSE IN MVP1 THERE IS NO CONFIG</param>
        /// <returns>Currency Entity</returns>
        public async Task<Currency> GetCurrencyByCodeAsync(string currencyCode, int validCacheSeconds)
        {
            // get ALWAYS the latest exchange rate from the remote source
            var lastExchangeRate = await exchangeRateProxy.GetExchangeRateAsync(currencyCode);

            // // IF not available check if there is a valid one in the cache
            // // (this mitigates the case the remote source is  temporary not available)

            if (lastExchangeRate == null)
            {
                if (!cache.TryGetValue(currencyCode, out lastExchangeRate))
                {
                    throw new ManagedException($"No exchange rate available for '{currencyCode}'", Consts.HttpStatusInternal.NotFound);
                }
            }
            else
            {
                if (!cache.TryGetValue(currencyCode, out ExchangeRate cachedExchangeRate))
                {
                    var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(
                            TimeSpan.FromSeconds(validCacheSeconds));
                    cache.Set(currencyCode, lastExchangeRate, options);
                }
            }

            // here depending on the currencyCode, we might need to create a specific Currency
            // such as CurrencyEur/CurrencyUsd etc. if the logic starts to diverge

            return new Currency(currencyCode, lastExchangeRate);
        }
    }
}