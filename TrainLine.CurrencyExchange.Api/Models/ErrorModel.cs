using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainLine.CurrencyExchange.Api.Models
{
    public class ErrorModel
    {
        public ErrorModel(bool isManaged = true) // horrible, i know
        {
            Type = "Unmanaged Error";
            if (isManaged)
                Type = "Managed Error";
        }

        public string Type { get; private set; }

        public string Message { get; set; }

        public object Payload { get; set; }
    }
}