using LGDShop.Domain.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Responses
{
    /// <summary>
    /// generally refer to errors that violate business logic rule
    /// </summary>
    public class ErrorResponse : ResponseBase
    {
        [JsonProperty("errors")]
        public string Type { get; set; }

        public ErrorResponse()
        {
            Type = ErrorType.General;
        }

        public ErrorResponse(string errorMessage, string errorType = ErrorType.General, string message = null)
        {
            Type = errorType;
            Message = message ?? errorMessage;
            if (ErrorMessages == null)
            {
                ErrorMessages = new List<string>();
            }
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                ErrorMessages.Add(errorMessage);
            }
            IsSuccessful = false;
        }
    }
}
