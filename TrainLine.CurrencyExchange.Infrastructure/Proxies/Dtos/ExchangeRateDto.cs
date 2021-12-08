using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Infrastructure.Proxies.Dtos
{
    //   {
    //"base": "EUR",
    //"date": "2021-04-07",
    //"time_last_updated": 1617753602,
    //"rates": {
    //	"GBP": 0.855552,
    //	"EUR": 1,
    //	"USD": 1.183894
    //        }
    //   }

    public class ExchangeRateDto
    {
        [JsonProperty(PropertyName = "base")]
        public string BaseCurrencyCode { get; set; }

        // data converter
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        //need an Epoch converter
        [JsonProperty(PropertyName = "time_last_updated")]
        public long LastUpdate { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public RateDto Rates { get; set; }
    }
}