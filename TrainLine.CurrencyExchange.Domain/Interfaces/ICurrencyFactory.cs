using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Entities;

namespace TrainLine.CurrencyExchange.Domain.Interfaces
{
    public interface ICurrencyFactory
    {
        Task<Currency> GetCurrencyByCodeAsync(string currencyCode, int validCacheSeconds);
    }
}