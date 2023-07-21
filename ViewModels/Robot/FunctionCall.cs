using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Robot
{
    [JsonObject]
    public class FunctionCall
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("arguments")]
        public string Arguments { get; set; }

        [JsonIgnore]
        public BotProcess ExecutingProcess { get; set; }

        [JsonIgnore]
        public string Result { get; set; }
    }
}
