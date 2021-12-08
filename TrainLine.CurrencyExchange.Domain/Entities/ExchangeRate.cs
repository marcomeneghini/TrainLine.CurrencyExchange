using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class ExchangeRate
    {
        public ExchangeRate()
        {
            Rates = new Dictionary<string, decimal>();
        }

        public string BaseCurrencyCode { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Contains the destination currencies code with correspondent exchange rate
        /// </summary>
        public Dictionary<string, decimal> Rates { get; set; }
    }
}