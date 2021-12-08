using System;
using System.Collections.Generic;
using System.Text;
using static TrainLine.CurrencyExchange.Domain.Entities.Consts;

namespace TrainLine.CurrencyExchange.Domain.Exceptions
{
    public class ManagedException : Exception
    {
        public HttpStatusInternal HttpCode { get; private set; }

        public ManagedException(string message, HttpStatusInternal httpCode = HttpStatusInternal.BadRequest) : base(message)
        {
            HttpCode = httpCode;
        }
    }
}