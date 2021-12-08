using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainLine.CurrencyExchange.Api.Models
{
    public class ExchangeResponseModel
    {
        public string SourceCurrencyCode { get; set; }

        public string DestinationCurrencyCode { get; set; }

        public decimal SourceAmount { get; set; }

        public decimal DestinationAmount { get; set; }

        public decimal CurrentExchangeRate { get; set; }
    }
}