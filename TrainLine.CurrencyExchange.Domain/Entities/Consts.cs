using System;
using System.Collections.Generic;
using System.Text;

namespace TrainLine.CurrencyExchange.Domain.Entities
{
    public class Consts
    {
        public const string EUR_CODE = "EUR";
        public const string GBP_CODE = "GBP";
        public const string USD_CODE = "USD";

        public enum HttpStatusInternal : int
        {
            BadRequest = 400,
            NotFound = 404,
            InternalServerError = 500,
        }
    }
}