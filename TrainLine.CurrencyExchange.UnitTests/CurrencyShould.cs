using System;
using System.Collections.Generic;
using System.Text;
using TrainLine.CurrencyExchange.Domain.Entities;
using TrainLine.CurrencyExchange.Domain.Exceptions;
using Xunit;
using static TrainLine.CurrencyExchange.Domain.Entities.Consts;

namespace TrainLine.CurrencyExchange.UnitTests
{
    public class CurrencyShould
    {
        private readonly string currencyCode = "currencyCode";
        private readonly Currency currency;
        private readonly ExchangeRate currentexchangeRate;
        private const decimal defaultValidAmount = 1;

        public CurrencyShould()
        {
            currentexchangeRate = new ExchangeRate();
            currency = new Currency(currencyCode, currentexchangeRate);
        }

        [Fact]
        public void GIVEN_ExchangeMoney_WHEN_AmountIsLEZero_THEN_ThrowManagedException400()
        {
            var exception = Assert.Throws<ManagedException>(() => currency.ExchangeMoney(currencyCode, 0));
            Assert.Equal(Consts.HttpStatusInternal.BadRequest, exception.HttpCode);
        }

        [Fact]
        public void GIVEN_ExchangeMoney_WHEN_DestinationCurrencyCodeNotPresent_THEN_ThrowManagedException404()
        {
            var notExistingDestinationCode = "notExistingDestinationCode";
            currentexchangeRate.Rates = new Dictionary<string, decimal>();
            var exception = Assert.Throws<ManagedException>(() => currency.ExchangeMoney(notExistingDestinationCode, defaultValidAmount));
            Assert.Equal(HttpStatusInternal.NotFound, exception.HttpCode);
        }

        [Fact]
        public void GIVEN_ExchangeMoney_WHEN_DestinationCurrencyExists_THEN_ReturnValidExchangeResponse()
        {
            var existingDestinationCode = "existingDestinationCode";
            currentexchangeRate.Rates = new Dictionary<string, decimal>()
            {
                {existingDestinationCode,defaultValidAmount }
            };

            var exchangeResponse = currency.ExchangeMoney(existingDestinationCode, defaultValidAmount);
            Assert.NotNull(exchangeResponse);
        }

        [Fact]
        public void GIVEN_ExchangeMoney_WHEN_DestinationCurrencyExists_AND_AmountIs2_AND_ExchangeRateIs3_THEN_ReturnsAmount6()
        {
            var existingDestinationCode = "existingDestinationCode";
            currentexchangeRate.Rates = new Dictionary<string, decimal>()
            {
                {existingDestinationCode,3 }
            };

            var exchangeResponse = currency.ExchangeMoney(existingDestinationCode, 2);
            Assert.NotNull(exchangeResponse);
            Assert.Equal(6, exchangeResponse.DestinationAmount);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 0.5, 0.5)]
        [InlineData(2, 2, 4)]
        [InlineData(12.5, 1.12345, 14.04312)] //14.043125 => verify the Round(5)
        public void GIVEN_ExchangeMoney_WHEN_DestinationCurrencyExists_AND_AmountIsX_AND_ExchangeRateIsY_THEN_ReturnsAmountXY(decimal sourceAmount, decimal exchangeRate, decimal destinationAmount)
        {
            var existingDestinationCode = "existingDestinationCode";
            currentexchangeRate.Rates = new Dictionary<string, decimal>()
            {
                {existingDestinationCode,exchangeRate }
            };

            var exchangeResponse = currency.ExchangeMoney(existingDestinationCode, sourceAmount);
            Assert.NotNull(exchangeResponse);
            Assert.Equal(destinationAmount, exchangeResponse.DestinationAmount);
        }
    }
}