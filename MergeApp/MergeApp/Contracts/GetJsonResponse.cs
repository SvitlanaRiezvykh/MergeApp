using Newtonsoft.Json;
using System.Collections.Generic;

namespace MergeApp.Contracts
{
    public partial class GetJsonResponse
    {
        [JsonProperty("ranked")]
        public List<Ranked> Ranked { get; set; }
    }
}
