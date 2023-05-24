using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request
{
    public class TireRequest
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("valueSet")]
        public ValueSetRequest ValueSet { get; set; }

        [JsonProperty("propertySet")]
        public PropertySetRequest PropertySet { get; set; }
    }
}