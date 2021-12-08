using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class ExchangeResponse
    {
        public string SourceCurrencyCode { get; set; }

        public string DestinationCurrencyCode { get; set; }

        public decimal SourceAmount { get; set; }

        public decimal DestinationAmount { get; set; }

        public decimal CurrentExchangeRate { get; set; }
    }
}