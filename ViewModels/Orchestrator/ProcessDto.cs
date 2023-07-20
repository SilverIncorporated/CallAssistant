using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class ProcessDto
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tags")]
        public List<TagDto> Tags { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("inputArguments")]
        public string InArguments { get; set; }

        [JsonProperty("outputArguments")]
        public string OutArguments { get; set; }

        [JsonProperty("arguments")]
        public ArgumentMetadataDto Arguments { get; set; }
    }
}
