using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class TagDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

    }
}
