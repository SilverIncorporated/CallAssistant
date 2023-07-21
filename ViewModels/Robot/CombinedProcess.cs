
using CallAssistant.ViewModels.Orchestrator;

namespace CallAssistant.ViewModels.Robot
{
    public class CombinedProcess
    {
        public CombinedProcess(BotProcess rjsProcess, ProcessDto? orchProcess)
        {
            RJSProcess = rjsProcess;
            if (orchProcess !=null && rjsProcess.Name != orchProcess.Name)
            {
                throw new ArgumentException("Processes do not match");
            }
            OrchProcess = orchProcess;
        }

        public BotProcess RJSProcess { get; }
        public ProcessDto? OrchProcess { get; set; }

        public bool Synched { get => OrchProcess != null; }

        public string Name { get => RJSProcess.Name; }
        public Guid Id { get => RJSProcess.Id; }

    }
}
