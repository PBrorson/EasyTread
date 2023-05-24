using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request
{
    public class ValueSetRequest
    {
        [JsonProperty("global")]
        public GlobalRequest Global { get; set; }

        [JsonProperty("minimum")]
        public MinimumRequest Minimum { get; set; }

        [JsonProperty("innerRegion")]
        public InnerRegionRequest InnerRegion { get; set; }

        [JsonProperty("middleRegion")]
        public MiddleRegionRequest MiddleRegion { get; set; }

        [JsonProperty("outerRegion")]
        public OuterRegionRequest OuterRegion { get; set; }
    }
}