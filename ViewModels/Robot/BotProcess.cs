using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Robot
{
    [JsonObject]
    public class BotProcess
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public string NameClean
        {
            get
            {
                return Name
                    .Replace(" ", "_")
                    .Replace(".", "_")
                    .Replace("?", "_")
                    .Replace("|", "_");
            }
        }
    }
}
