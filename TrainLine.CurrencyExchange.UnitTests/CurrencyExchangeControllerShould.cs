using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Api.Controllers;
using TrainLine.CurrencyExchange.Api.MappingProfiles;
using TrainLine.CurrencyExchange.Domain.Exceptions;
using TrainLine.CurrencyExchange.Domain.Interfaces;
using Xunit;
using static TrainLine.CurrencyExchange.Domain.Entities.Consts;

namespace TrainLine.CurrencyExchange.UnitTests
{
    public class CurrencyExchangeControllerShould
    {
        private readonly CurrencyExchangeController currencyExchangeController;
        private readonly IMapper _mapper;
        private readonly Mock<ICurrencyFactory> currencyFactoryMock;
        private const decimal defaultValidAmount = 1;
        private const string defaultMessage = "defaultMessage";

        public CurrencyExchangeControllerShould()
        {
            var proxyProfile = new ModelDomainProfile(); // using Real Automapper Profile, not a Mock
            var mapperConfiguration = new MapperConfiguration(cfg =>
            cfg.AddProfile(proxyProfile));
            _mapper = new Mapper(mapperConfiguration);

            currencyFactoryMock = new Mock<ICurrencyFactory>();

            currencyExchangeController = new CurrencyExchangeController(currencyFactoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GIVEN_GetCurrencyExchange_WHEN_GetCurrencyByCodeAsyncThrowsMangedException404_THEN_Returns404()
        {
            currencyFactoryMock.Setup(x => x.GetCurrencyByCodeAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new ManagedException(defaultMessage, Domain.Entities.Consts.HttpStatusInternal.NotFound));

            var response = await currencyExchangeController.GetCurrencyExchange("", "", defaultValidAmount);
            Assert.NotNull(response);
            var typedResponse = Assert.IsType<ObjectResult>(response);
            Assert.Equal(((int)HttpStatusInternal.NotFound), typedResponse.StatusCode);
        }
    }
}