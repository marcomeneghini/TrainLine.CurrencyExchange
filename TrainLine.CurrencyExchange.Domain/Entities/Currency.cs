using System;
using System.Collections.Generic;
using System.Text;
using TrainLine.CurrencyExchange.Domain.Exceptions;

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
            if (amount <= 0)
                throw new ManagedException($"Cannot exchange zero or negative amount '{amount}'", Consts.HttpStatusInternal.BadRequest);

            if (!CurrentExchangeRates.Rates.TryGetValue(destinationCurrencyCode, out decimal destinationCurrencyExchangeRate))
                throw new ManagedException($"No exchange rate from '{Code}' to '{destinationCurrencyCode}'", Consts.HttpStatusInternal.NotFound);

            var destinationAmount = Decimal.Round(amount * destinationCurrencyExchangeRate, 5);

            return new ExchangeResponse
            {
                CurrentExchangeRate = destinationCurrencyExchangeRate,
                SourceAmount = amount,
                DestinationAmount = destinationAmount,
                DestinationCurrencyCode = destinationCurrencyCode,
                SourceCurrencyCode = Code
            };
        }
    }
}