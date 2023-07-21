using Microsoft.JSInterop;
using CallAssistant.ViewModels.Robot;
using Newtonsoft.Json;
using CallAssistant.ViewModels.Orchestrator;

namespace CallAssistant.Helpers
{
    public class RobotJsHelper
    {
        public RobotJsHelper(IJSRuntime runtime, Action render)
        {
            _render = render;
            _runtime = runtime;
            AvailableProcesses = new List<BotProcess>();
        }
        public IEnumerable<BotProcess> AvailableProcesses { get; set; }
        private Action _render;
        private Action _getProcessesReturn;
        private IJSRuntime _runtime;

        private Dictionary<string, Action<BotProcess,string, FunctionCall>> startProcessCallbacks = new Dictionary<string, Action<BotProcess,string, FunctionCall>>();
        private Dictionary<string, Action<BotProcess, string>> installProcessCallbacks = new Dictionary<string, Action<BotProcess, string>>();

        public async ValueTask GetProcesses(Action callback)
        {
            _getProcessesReturn = callback;
            var objectRef = DotNetObjectReference.Create(this);
            await _runtime.InvokeVoidAsync("GetProcesses", objectRef);
        }
        public async ValueTask StartProcess(FunctionCall call)
        {
            var objectRef = DotNetObjectReference.Create(this);
            await _runtime.InvokeAsync<string>("RunProcess", objectRef, call.ExecutingProcess, call);
        }

        public async ValueTask StartProcess(FunctionCall call, Action<BotProcess, string, FunctionCall> callback)
        {
            startProcessCallbacks[call.ExecutingProcess.Id.ToString()] = callback;
            var objectRef = DotNetObjectReference.Create(this);
            await _runtime.InvokeAsync<string>("RunProcess", objectRef, call.ExecutingProcess, call);
        }

        public async ValueTask InstallProcess(BotProcess process, Action<BotProcess, string> callback)
        {
            var objectRef = DotNetObjectReference.Create(this);
            installProcessCallbacks[process.Id.ToString()] = callback;
            await _runtime.InvokeVoidAsync("InstallProcess", objectRef, process);
        }

        [JSInvokable("onGetProcessReturn")]
        public async Task OnGetProcessReturn(List<BotProcess> processes)
        {
            AvailableProcesses = processes;
            _getProcessesReturn();
        }

        [JSInvokable("onProcessFinish")]
        public async Task OnProcessFinish(string botProcessJson, string result, string callJson)
        {

            var call = JsonConvert.DeserializeObject<FunctionCall>(callJson);
            var proc = JsonConvert.DeserializeObject<BotProcess>(botProcessJson);
            startProcessCallbacks[proc.Id.ToString()](proc, result, call);
        }

        [JSInvokable("onProcessInstall")]
        public async Task OnProcessInstall(string botProcessJson, string result)
        {
            var proc = JsonConvert.DeserializeObject<BotProcess>(botProcessJson);

            installProcessCallbacks[proc.Id.ToString()](proc, result);
        }

    }
}
