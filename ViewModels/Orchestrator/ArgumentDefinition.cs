using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CallAssistant.ViewModels.Orchestrator
{
    [JsonObject]
    public class ArgumentDefinition
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        private string type;
        [Newtonsoft.Json.JsonIgnore]
        public string Type { get => type.Split(",").First().Split(".").Last().ToLower(); }
        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("isRequired")]
        private bool isRequired { set => Required = value; }
    }
}
