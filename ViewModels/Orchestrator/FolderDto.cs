using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class FolderDto
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
