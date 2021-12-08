using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Entities;

namespace TrainLine.CurrencyExchange.Domain.Interfaces
{
    public interface IExchangeRateProxy
    {
        Task<ExchangeRate> GetExchangeRateAsync(string currencyCode);
    }
}