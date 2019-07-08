using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LGDShop.API.Models.V1.Responses
{
    /// <summary>
    /// delete entity base response
    /// </summary>
    public class EntityDeleteResponse : ResponseBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("object")]
        public string Entity { get; set; }     //entity type  e.g. employee, department etc.
        [JsonProperty("deleted")]
        public bool IsDeleted { get; set; }
    }
}
