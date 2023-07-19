using Newtonsoft.Json;
using System.Collections.Generic;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class ProcessDto
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("resourceTags")]
        public List<TagDto> Tags { get; set; }
    }
}
