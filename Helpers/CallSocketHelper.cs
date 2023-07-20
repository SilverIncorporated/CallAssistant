using Microsoft.JSInterop;
using CallAssistant.ViewModels;
using CallAssistant.ViewModels.Orchestrator;
using System;

namespace CallAssistant.Helpers
{
    public class CallSocketHelper
    {
        public CallSocketHelper(IJSRuntime jsRuntime, string url)
        {
            _jsRuntime = jsRuntime;
            _url = url;
        }

        private IJSRuntime _jsRuntime;
        private string _url;

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


    }
}
