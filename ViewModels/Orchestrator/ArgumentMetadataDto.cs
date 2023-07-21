using Newtonsoft.Json;

namespace CallAssistant.ViewModels.Orchestrator
{
    public class ArgumentMetadataDto
    {
        private string input;
        private string output;
        public string Input { get => input; set
            {
                input = value;
                InArguments = JsonConvert.DeserializeObject<ArgumentDefinition[]>(value);
            }
        }
        public string Output { get => output; set
            {
                output = value;
                OutArguments = JsonConvert.DeserializeObject<ArgumentDefinition[]>(value);
            }
        }

        public IEnumerable<ArgumentDefinition> InArguments { get; set; }

        public IEnumerable<ArgumentDefinition> OutArguments { get; set; }
    }
}
