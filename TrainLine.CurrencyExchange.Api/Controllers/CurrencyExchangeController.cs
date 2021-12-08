using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Api.Models;
using TrainLine.CurrencyExchange.Domain.Exceptions;
using TrainLine.CurrencyExchange.Domain.Interfaces;

namespace TrainLine.CurrencyExchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyFactory currencyFactory;
        private readonly IMapper mapper;
        private const int validCache24hours = 60 * 60 * 24; // ss * mm * HH

        public CurrencyExchangeController(
            ICurrencyFactory currencyFactory,
            IMapper mapper)
        {
            this.currencyFactory = currencyFactory;
            this.mapper = mapper;
        }

        // need to validate the input
        [HttpGet]
        public async Task<IActionResult> GetCurrencyExchange([FromQuery] string source, [FromQuery] string destination, [FromQuery] decimal amount)
        {
            try
            {
                var sourcecurrency = await currencyFactory.GetCurrencyByCodeAsync(source, validCache24hours);

                var exchageResponse = sourcecurrency.ExchangeMoney(destination, amount);

                return Ok(mapper.Map<ExchangeResponseModel>(exchageResponse));
            }
            catch (ManagedException e)
            {
                return StatusCode(((int)e.HttpCode), new ErrorModel { Message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorModel(false) { Message = e.Message });
            }
        }
    }
}