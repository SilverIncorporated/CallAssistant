using Microsoft.JSInterop;
using CallAssistant.ViewModels.Robot;
using CallAssistant.ViewModels.Orchestrator;

namespace CallAssistant.Helpers
{
    static class CallSocketExtensions
    {

        public static async ValueTask RegisterFunction(this IJSRuntime jsRuntime, object reference, ProcessDto process)
        {
            var objectRef = DotNetObjectReference.Create(reference);
            await jsRuntime.InvokeVoidAsync("RegisterFunction", objectRef, process);
        }

    }
}
