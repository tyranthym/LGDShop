using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LGDShop.Domain
{
    public class AppSettings
    {
        [JsonProperty("apiDomain")]
        public string ApiDomain { get; set; }
    }
}
