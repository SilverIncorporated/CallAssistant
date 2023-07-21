using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class FolderDto
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }
    }
}
