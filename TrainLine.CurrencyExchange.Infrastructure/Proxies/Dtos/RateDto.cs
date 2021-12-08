using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Infrastructure.Proxies.Dtos
{
    public class RateDto
    {
        [JsonProperty(PropertyName = "USD")]
        public decimal UsdRate { get; set; }

        [JsonProperty(PropertyName = "GBP")]
        public decimal GbpRate { get; set; }

        [JsonProperty(PropertyName = "EUR")]
        public decimal EurRate { get; set; }
    }
}