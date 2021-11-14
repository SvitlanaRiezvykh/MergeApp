using Newtonsoft.Json;
using System.Collections.Generic;

namespace MergeApp.Contracts
{
    public partial class Ranked
    {
        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("vals")]
        public IDictionary<string, object> Vals { get; set; }
    }
}