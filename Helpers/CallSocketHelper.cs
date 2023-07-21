using Microsoft.JSInterop;
using CallAssistant.ViewModels.Call;
using CallAssistant.ViewModels.Orchestrator;
using Newtonsoft.Json;
using CallAssistant.ViewModels.Robot;
using Microsoft.AspNetCore.Components;

namespace CallAssistant.Helpers
{
    public class CallSocketHelper
    {
        public CallSocketHelper(IJSRuntime jsRuntime, string url, Action[] Render, Action<FunctionCall> functionCall)
        {
            _jsRuntime = jsRuntime;
            _url = url;
            _render = Render;
            Transcriptions = new List<Transcription>();
            _functionCall = functionCall;
        }

        private IJSRuntime _jsRuntime;
        private string _url;
        private Action[] _render;
        private Action<FunctionCall> _functionCall;
        public Action SocketConnectCallback;

        public List<Transcription> Transcriptions { get; set; }

        public async Task StartSocket()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("WSClose");
            }
            catch { }
            var objectRef = DotNetObjectReference.Create(this);
            await _jsRuntime.InvokeVoidAsync("WSInitialize", objectRef, _url);
        }
        public async Task CloseSocket()
        {
            await _jsRuntime.InvokeVoidAsync("WSClose");
        }

        public async Task RegisterFunction(ProcessDto process)
        {
            await _jsRuntime.InvokeVoidAsync("RegisterFunction", process);
        }

        public async Task FunctionCompletion(FunctionCall call)
        {
            await _jsRuntime.InvokeVoidAsync("FunctionComplete", call);
        }

        public async Task SendMessage(string Message)
        {
            await _jsRuntime.InvokeVoidAsync("SendMessage", Message);
        }

        [JSInvokable("onTranscription")]
        public async Task OnTranscription(string transcriptionJson)
        {
            var transcription = JsonConvert.DeserializeObject<Transcription>(transcriptionJson);
            Transcriptions.Add(transcription);
            foreach (var r in _render)
            {
                r();
            }
        }

        [JSInvokable("onFunctionCall")]
        public async Task OnFunctionCall(string functionParams)
        {
            var funcCall = JsonConvert.DeserializeObject<FunctionCall>(functionParams);
            funcCall.Id = Guid.NewGuid();
            _functionCall(funcCall);
        }

        [JSInvokable("onSocketConnect")]
        public async Task OnSocketConnect()
        {
            if (OnSocketConnect != null)
            {
                SocketConnectCallback();
            }
        }
    }
}
