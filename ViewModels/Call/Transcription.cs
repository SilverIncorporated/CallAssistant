using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Call
{
    [JsonObject]
    public class Transcription
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
