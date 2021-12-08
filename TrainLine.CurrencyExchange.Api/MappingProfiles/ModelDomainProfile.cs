using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainLine.CurrencyExchange.Api.Models;
using TrainLine.CurrencyExchange.Domain.Entities;

namespace TrainLine.CurrencyExchange.Api.MappingProfiles
{
    public class ModelDomainProfile : Profile
    {
        public ModelDomainProfile()
        {
            CreateMap<ExchangeResponseModel, ExchangeResponse>().ReverseMap();
        }
    }
}