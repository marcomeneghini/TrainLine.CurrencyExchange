using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class Currency
    {
        public Currency(string code, ExchangeRate exchangeRate)
        {
            this.Code = code;
            this.CurrentExchangeRates = exchangeRate;
        }

        public string Code { get; private set; }

        public ExchangeRate CurrentExchangeRates { get; private set; }

        // this method is virtual because we might need to extend this class
        // to implement specific behaviours for different currencies
        // Example : CurrencyUsd:Currency
        public virtual ExchangeResponse ExchangeMoney(string destinationCurrencyCode, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}