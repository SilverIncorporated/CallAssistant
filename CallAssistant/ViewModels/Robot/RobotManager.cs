using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CallAssistant.ViewModels.Robot
{
    public class RobotManager
    {
        public RobotManager(Action messageCallback)
        {
            MessageCallback = messageCallback;
        }

        private Action MessageCallback { get; set; }
        public List<BotProcess> Processes { get; set; } = new List<BotProcess>();
        public string Message { get; set; } = "";
        public EventCallback OnChange { get; set; }

        [JSInvokable("onGetProcessReturn")]
        public void SetProcesses(List<BotProcess> processes)
        {
            Processes = processes.OrderBy(p => p.Name).ToList();
            MessageCallback();
        }

        [JSInvokable("jobFinished")]
        public void OnJobFinish(string result)
        {
            Message = result;
            MessageCallback();
        }
    }
}
