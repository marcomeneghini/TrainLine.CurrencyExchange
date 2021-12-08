using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Domain.Entities;
using Xunit;

namespace TrainLine.CurrencyExchange.UnitTests
{
    public class CurrencyFactoryShould
    {
        private readonly CurrencyFactory currencyFactory;

        public CurrencyFactoryShould()
        {
        }

        [Fact]
        public async Task GIVEN_GetCurrencyByCode_WHEN_SourceCurrencyNotExistsRemotely_AND_NotExistsInCache_THEN_ThrowsManagedException404()
        {
            await currencyFactory.GetCurrencyByCodeAsync("", 0);
        }

        [Fact]
        public async Task GIVEN_GetCurrencyByCode_WHEN_SourceCurrencyNotExistsRemotely_AND_ExistsInCache_THEN_ReturnCurrency()
        {
            await currencyFactory.GetCurrencyByCodeAsync("", 0);
        }

        [Fact]
        public async Task GIVEN_GetCurrencyByCode_WHEN_SourceCurrencyExistsRemotely_THEN_ReturnCurrency()
        {
            await currencyFactory.GetCurrencyByCodeAsync("", 0);
        }
    }
}