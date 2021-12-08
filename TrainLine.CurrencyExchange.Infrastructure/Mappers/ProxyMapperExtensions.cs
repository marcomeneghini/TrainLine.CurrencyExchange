using System;
using System.Collections.Generic;
using System.Text;
using TrainLine.CurrencyExchange.Domain.Entities;
using TrainLine.CurrencyExchange.Infrastructure.Proxies.Dtos;

namespace TrainLine.CurrencyExchange.Infrastructure.Mappers
{
    public static class ProxyMapperExtensions
    {
        public static ExchangeRate ToDomain(this ExchangeRateDto dto) => new ExchangeRate
        {
            BaseCurrencyCode = dto.BaseCurrencyCode,
            Date = dto.Date,
            LastUpdate = DateTimeOffset.FromUnixTimeSeconds(dto.LastUpdate).DateTime,  // epochToDatetime
            Rates = new Dictionary<string, decimal>
                {
                    {Consts.EUR_CODE,dto.Rates.EurRate },
                    {Consts.GBP_CODE,dto.Rates.GbpRate },
                    {Consts.USD_CODE,dto.Rates.UsdRate }
                }
        };
    }
}