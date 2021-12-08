using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Interfaces;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class CurrencyFactory : ICurrencyFactory
    {
        public CurrencyFactory()
        {
        }

        public Task<Currency> GetCurrencyByCodeAsync(string currencyCode, int validCacheSeconds)
        {
            throw new NotImplementedException();
            // get ALWAYS the latest exchange rate from the remote source

            // // IF not available check if there is a valid one in the cache
            // // (this mitigates the case the remote source is  temporary not available)
        }
    }
}